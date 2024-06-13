using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ejada.TaskManagement;

[Dependency(ReplaceServices = true)]
public class TaskManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TaskManagement";
}
