using System;
using System.Collections.Generic;
using System.Text;

namespace Ejada.TaskManagement.Tasks
{
    public class AttachmentDto
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
