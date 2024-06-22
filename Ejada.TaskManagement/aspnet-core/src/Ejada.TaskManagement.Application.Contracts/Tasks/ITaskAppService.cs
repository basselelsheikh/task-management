using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Ejada.TaskManagement.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<PagedResultDto<TaskDto>> GetTasksAssignedToUserAsync(GetTaskListDto input);
        Task<TaskDto> GetTaskAsync(Guid id);
        Task<TaskStatus> StartTask(Guid id);
        Task<TaskStatus> FinishTask(Guid id);
        Task<IRemoteStreamContent> DownloadAttachment(string blobName);
        Task<ListResultDto<EmployeeLookupDto>> GetEmployeeLookupAsync();
        Task CreateTaskAsync(CreateTaskDto input);
    }
}
