using Ejada.TaskManagement.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Task = Ejada.TaskManagement.Tasks.Task;

namespace Ejada.TaskManagement
{
    public class TaskManagementDataSeederContributor(ITaskRepository taskRepository, IGuidGenerator guidGenerator, IBlobContainer<AttachmentContainer> blobContainer, IRepository<Attachment, Guid> attachmentRepository) : IDataSeedContributor, ITransientDependency
    {
        public async System.Threading.Tasks.Task SeedAsync(DataSeedContext context)
        {
            if (await taskRepository.GetCountAsync() <= 0)
            {
                var task1 = new Task(guidGenerator.Create(), "Make Task Management application using abp", DateTime.Now.AddDays(2), TaskPriority.Medium, new Guid("57496BCE-A02D-8B60-F994-3A12F4EE424B"));
                task1.AsssignTaskToEmployee(new Guid("9AB413D0-6A93-6CC0-F333-3A131447D089"));
                var attachment = new Attachment(guidGenerator.Create(), task1.Id, "Projects.docx", "Projects.docx", 1, "docx");
                var fileBytes = File.ReadAllBytes("D:\\abp-task-management\\Ejada.TaskManagement\\aspnet-core\\src\\Ejada.TaskManagement.Domain\\Projects.docx");
                await taskRepository.InsertAsync(task1, autoSave: true);
                await attachmentRepository.InsertAsync(attachment, autoSave: true);
                await blobContainer.SaveAsync("Projects.docx", fileBytes);
            }
        }
    }
}