using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Content;

namespace Ejada.TaskManagement.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        public String Name { get; set; }
        public String? Description { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public IEnumerable<IRemoteStreamContent> Attachments { get; set; }
    }

}
