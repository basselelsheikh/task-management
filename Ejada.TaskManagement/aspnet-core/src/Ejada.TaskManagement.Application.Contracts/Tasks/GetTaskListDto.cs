using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ejada.TaskManagement.Tasks
{
    public class GetTaskListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
