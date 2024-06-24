using Ejada.TaskManagement.Permissions;
using Ejada.TaskManagement.Tasks.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Ejada.TaskManagement.Tasks
{
    [Authorize(TaskManagementPermissions.Tasks.Default)]
    public class TaskAppService(ITaskRepository taskRepository, ICurrentUser currentUser, IRepository<Attachment, Guid> attachmentRepository, IBlobContainer<AttachmentContainer> blobContainer, UserManager<Volo.Abp.Identity.IdentityUser> userManager) : TaskManagementAppService, ITaskAppService
    {
        [Authorize(TaskManagementPermissions.Tasks.ViewAssigned)]
        public async Task<PagedResultDto<TaskDto>> GetTasksAssignedToUserAsync(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {       
                input.Sorting = nameof(Task.DueDate);
            }

            var (tasks, count) = await taskRepository.GetTasksAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter,
                new AssignedTasksToEmployeeSpecification((Guid)currentUser.Id!)
                );

            var taskDtos = tasks.Select(t =>
            {
                var taskDto = ObjectMapper.Map<Task, TaskDto>(t.Task);
                taskDto.CreatorUserName = t.CreatorUserName;
                return taskDto;

            }).ToList();

            return new PagedResultDto<TaskDto>(
                count,
                taskDtos
            );
        }

        [Authorize(TaskManagementPermissions.Tasks.ViewAll)]
        public async Task<PagedResultDto<TaskDto>> GetAllTasks(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Task.DueDate);
            }

            var (tasks, count) = await taskRepository.GetTasksAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
                );

            var taskDtos = tasks.Select(t =>
            {
                var taskDto = ObjectMapper.Map<Task, TaskDto>(t.Task);
                taskDto.CreatorUserName = t.CreatorUserName;
                taskDto.AssigneeUserName = t.AssigneeUserName;
                return taskDto;

            }).ToList();

            return new PagedResultDto<TaskDto>(
                count,
                taskDtos
            );
        }

        public async Task<TaskDto> GetTaskAsync(Guid id)
        {
            var result = await taskRepository.GetTaskWithDetails(id);
            var taskDto = ObjectMapper.Map<Task, TaskDto>(result.Task);
            taskDto.CreatorUserName = result.CreatorUserName;
            taskDto.AssigneeUserName = result.AssigneeUserName;
            return taskDto;
        }

        [Authorize(TaskManagementPermissions.Tasks.StartTask)]
        public async Task<TaskStatus> StartTask(Guid id)
        {
            var task = await taskRepository.GetAsync(id);
            task.StartTask();
            return task.Status;
        }

        [Authorize(TaskManagementPermissions.Tasks.FinishTask)]
        public async Task<TaskStatus> FinishTask(Guid id)
        {
            var task = await taskRepository.GetAsync(id);
            task.FinishTask();
            return task.Status;
        }

        public async Task<ListResultDto<EmployeeLookupDto>> GetEmployeeLookupAsync()
        {
            var employees = await userManager.GetUsersInRoleAsync("employee");
            var employeeLookupDtos = employees.Select(u =>
            new EmployeeLookupDto
            {
                Id = u.Id,
                Name = string.IsNullOrEmpty(u.Name) ? u.UserName : u.Name + " " + u.Surname,
            }).ToList();
            return new ListResultDto<EmployeeLookupDto>(employeeLookupDtos);
        }

        [Authorize(TaskManagementPermissions.Tasks.Create)]
        public async System.Threading.Tasks.Task CreateTaskAsync(CreateTaskDto input)
        {
            var task = new Task(GuidGenerator.Create(), input.Name, input.DueDate, input.Priority, (Guid)currentUser.Id!, input.Description);

            if (input.EmployeeId != null)
            {
                task.AsssignTaskToEmployee((Guid)input.EmployeeId);
            }

            await taskRepository.InsertAsync(task);

            if (input.Attachments != null && input.Attachments.Any())
            {
                
                foreach (var attachment in input.Attachments)
                {
                    var attachmentMetaData = new Attachment(GuidGenerator.Create(), task.Id, attachment.FileName!);
                    await blobContainer.SaveAsync(attachment.FileName!, attachment.GetStream(), true);
                    await attachmentRepository.InsertAsync(attachmentMetaData);
                }
            }

        }

        public async Task<IRemoteStreamContent> DownloadAttachment(string blobName)
        {
            var stream = await blobContainer.GetAsync(blobName);
            var remoteStream = new RemoteStreamContent(stream, blobName, "application/octet-stream");

            return remoteStream;
        }
    }
}