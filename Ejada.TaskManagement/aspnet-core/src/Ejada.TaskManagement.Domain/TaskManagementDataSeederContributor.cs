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
                var task1 = new Task(guidGenerator.Create(), "Make Task Management application using abp", DateTime.Now.AddDays(2), TaskPriority.Medium, new Guid("4e9e6edc-7485-8566-ccb3-3a132a53b8e6"));
                task1.AsssignTaskToEmployee(new Guid("c12227d9-4b37-3f5e-8d29-3a132a901fb2"));
                var attachment = new Attachment(guidGenerator.Create(), task1.Id, "sample.pdf", "sample.pdf", 1, "docx");
                var fileBytes = File.ReadAllBytes("C:\\sample.pdf");
                await taskRepository.InsertAsync(task1, autoSave: true);
                await attachmentRepository.InsertAsync(attachment, autoSave: true);
                await blobContainer.SaveAsync("sample.pdf", fileBytes);
            }
        }
    }
}