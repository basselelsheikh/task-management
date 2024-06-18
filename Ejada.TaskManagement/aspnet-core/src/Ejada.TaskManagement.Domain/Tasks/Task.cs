using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Ejada.TaskManagement.Tasks
{
    public class Task : AggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }
        public virtual string? Description { get; protected set; }
        public virtual DateTime DueDate { get; protected set; }
        public virtual TaskPriority Priority { get; protected set; }
        public virtual TaskStatus Status { get; protected set; }
        public virtual Guid CreatorUserId { get; protected set; }
        public virtual Guid? AssigneeUserId { get; protected set; }
        public virtual ICollection<Comment> Comments { get; protected set; }
        public virtual ICollection<Attachment> Attachments { get; protected set; }

        public Task(Guid id, string name, DateTime dueDate, TaskPriority priority, Guid creatorUserId, string? description = null)
        {

            Id = id;
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            if (DateTime.Compare(dueDate.Date, DateTime.Now.Date) < 0)
            {
                throw new BusinessException(TaskManagementDomainErrorCodes.TaskDueDateInThePast);
            }
            DueDate = dueDate;
            Priority = priority;
            Description = description;
            Status = TaskStatus.Pending;
            CreatorUserId = creatorUserId;
            Comments = new Collection<Comment>();
            Attachments = new Collection<Attachment>();
        }

        public void AsssignTaskToEmployee(Guid employeeId)
        {
            AssigneeUserId = employeeId;
        }
        public void StartTask()
        {
            if (Status != TaskStatus.Pending)
            {
                throw new BusinessException(TaskManagementDomainErrorCodes.InvalidTaskStatusException);
            }

            Status = TaskStatus.InProgress;
        }

        public void FinishTask()
        {
            if (Status != TaskStatus.InProgress)
            {
                throw new BusinessException(TaskManagementDomainErrorCodes.InvalidTaskStatusException);
            }

            Status = TaskStatus.Completed;
        }
        protected Task() { }
    }
}
