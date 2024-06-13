using Ejada.TaskManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ejada.TaskManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TaskManagementEntityFrameworkCoreModule),
    typeof(TaskManagementApplicationContractsModule)
    )]
public class TaskManagementDbMigratorModule : AbpModule
{
}
