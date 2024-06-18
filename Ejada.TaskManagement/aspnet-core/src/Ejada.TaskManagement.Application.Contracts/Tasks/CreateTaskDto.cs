using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ejada.TaskManagement.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public List<CreateTaskAttachmentDto> Attachments { get; set; }
    }
    public class CreateTaskAttachmentDto
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}
