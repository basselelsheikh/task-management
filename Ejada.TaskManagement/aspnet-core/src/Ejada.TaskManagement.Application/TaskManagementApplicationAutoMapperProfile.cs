using AutoMapper;
using Ejada.TaskManagement.Tasks;

namespace Ejada.TaskManagement;

public class TaskManagementApplicationAutoMapperProfile : Profile
{
    public TaskManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Task, TaskDto>();
        CreateMap<Attachment, TaskDto.AttachmentDto>();
    }
}
