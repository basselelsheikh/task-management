using Volo.Abp.Modularity;

namespace Ejada.TaskManagement;

[DependsOn(
    typeof(TaskManagementDomainModule),
    typeof(TaskManagementTestBaseModule)
)]
public class TaskManagementDomainTestModule : AbpModule
{

}
