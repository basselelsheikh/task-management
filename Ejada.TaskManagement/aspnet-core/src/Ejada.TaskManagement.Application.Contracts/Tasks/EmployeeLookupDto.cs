using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ejada.TaskManagement.Tasks
{
    public class EmployeeLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
