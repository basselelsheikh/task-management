using Volo.Abp.Modularity;

namespace Ejada.TaskManagement;

[DependsOn(
    typeof(TaskManagementApplicationModule),
    typeof(TaskManagementDomainTestModule)
)]
public class TaskManagementApplicationTestModule : AbpModule
{

}
