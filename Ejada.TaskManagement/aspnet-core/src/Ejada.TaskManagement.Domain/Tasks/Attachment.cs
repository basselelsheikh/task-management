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
        public virtual string FileName { get; protected set; }
        public virtual string BlobName { get; protected set; }
        public virtual long Size { get; protected set; }
        public virtual string ContentType { get; protected set; }
        public Attachment(Guid id, Guid taskId, string fileName, string blobName, long size, string contentType)
        {
            Id = id;
            TaskId = taskId;
            FileName =  Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
            BlobName = Check.NotNullOrWhiteSpace(blobName, nameof(blobName));
            Size = size;
            ContentType = Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        }
        protected Attachment() { }
    }
}
