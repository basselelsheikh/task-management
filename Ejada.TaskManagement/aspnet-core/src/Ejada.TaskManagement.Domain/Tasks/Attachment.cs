using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Ejada.TaskManagement.Tasks
{
    public class Attachment : Entity<Guid>
    {
        public Guid TaskId { get; set; }
        public virtual string BlobName { get; protected set; }
        public Attachment(Guid id, Guid taskId, string blobName)
        {
            Id = id;
            TaskId = taskId;
            BlobName = Check.NotNullOrWhiteSpace(blobName, nameof(blobName));
        }
        protected Attachment() { }
    }
}
