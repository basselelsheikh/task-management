using Ejada.TaskManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ejada.TaskManagement.Permissions;

public class TaskManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TaskManagementPermissions.GroupName, L("Permission:TaskManagement"));

        var tasksPermission = myGroup.AddPermission(TaskManagementPermissions.Tasks.Default, L("Permission:Tasks"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.ViewAll, L("Permission:Tasks.ViewAll"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.ViewAssigned, L("Permission:Tasks.ViewAssigned"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.Assign, L("Permission:Tasks.Assign"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.Create, L("Permission:Tasks.Create"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.StartTask, L("Permission:Tasks.StartTask"));
        tasksPermission.AddChild(TaskManagementPermissions.Tasks.FinishTask, L("Permission:Tasks.FinishTask"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TaskManagementResource>(name);
    }
}
