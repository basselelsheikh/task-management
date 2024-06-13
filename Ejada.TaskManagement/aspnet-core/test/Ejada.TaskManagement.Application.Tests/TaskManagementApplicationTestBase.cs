using Volo.Abp.Modularity;

namespace Ejada.TaskManagement;

public abstract class TaskManagementApplicationTestBase<TStartupModule> : TaskManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
