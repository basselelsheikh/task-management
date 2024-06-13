using Ejada.TaskManagement.Permissions;
using Ejada.TaskManagement.Tasks.Specifications;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Ejada.TaskManagement.Tasks
{
    [Authorize(TaskManagementPermissions.Tasks.Default)]
    public class TaskAppService(ITaskRepository taskRepository, ICurrentUser currentUser, IRepository<Attachment, Guid> attachmentRepository, IBlobContainer<AttachmentContainer> blobContainer) : TaskManagementAppService, ITaskAppService
    {
        [Authorize(TaskManagementPermissions.Tasks.ViewAssigned)]
        public async Task<PagedResultDto<TaskDto>> GetTasksAssignedToUserAsync(GetTaskListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Task.DueDate);
            }

            var (tasks, count) = await taskRepository.GetTasksAsync(
                new AssignedTasksToEmployeeSpecification((Guid)currentUser.Id!),
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
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

        public async Task<AttachmentDto> GetAttachments(Guid id)
        {
            var at = await attachmentRepository.GetAsync(a => a.TaskId == id);
            var blob = await blobContainer.GetAllBytesAsync(at.FileName);
            return new AttachmentDto
            {
                Content = blob
            };
        }
    }
}