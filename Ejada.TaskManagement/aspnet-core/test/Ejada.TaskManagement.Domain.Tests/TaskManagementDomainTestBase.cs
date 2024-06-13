using Volo.Abp.Modularity;

namespace Ejada.TaskManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class TaskManagementDomainTestBase<TStartupModule> : TaskManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
