using System.Threading.Tasks;

namespace Ejada.TaskManagement.Data;

public interface ITaskManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
