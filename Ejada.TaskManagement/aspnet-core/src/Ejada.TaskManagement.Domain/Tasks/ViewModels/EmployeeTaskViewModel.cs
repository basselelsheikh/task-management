using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejada.TaskManagement.Tasks.ViewModels
{
    public class EmployeeTaskViewModel
    {
        public Task Task { get; set; } = null!;
        public string CreatorUserName { get; set; } = null!;
        public string? AssigneeUserName { get; set; }
    }
}
