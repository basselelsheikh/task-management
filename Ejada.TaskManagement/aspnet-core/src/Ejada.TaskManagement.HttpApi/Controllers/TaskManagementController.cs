using Ejada.TaskManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ejada.TaskManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TaskManagementController : AbpControllerBase
{
    protected TaskManagementController()
    {
        LocalizationResource = typeof(TaskManagementResource);
    }
}
