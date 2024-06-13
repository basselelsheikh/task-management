using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Ejada.TaskManagement.Tasks
{
    public class Comment : Entity<Guid>
    {
        public virtual Guid TaskId { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual string Content { get; protected set; }
        public DateTime Created { get; protected set; }
        public Comment(Guid id, string content, Guid userId)
        {
            Id = id;
            Content = Check.NotNullOrWhiteSpace(content, nameof(content));
            Created = DateTime.UtcNow;
        }
        protected Comment() { }

    }
}
