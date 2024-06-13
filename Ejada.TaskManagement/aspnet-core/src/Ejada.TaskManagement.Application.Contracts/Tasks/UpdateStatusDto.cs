using System;
using System.Collections.Generic;
using System.Text;

namespace Ejada.TaskManagement.Tasks
{
    public class UpdateStatusDto
    {
        public Guid taskId {  get; set; }
        public TaskStatus status { get; set; }

    }
}
