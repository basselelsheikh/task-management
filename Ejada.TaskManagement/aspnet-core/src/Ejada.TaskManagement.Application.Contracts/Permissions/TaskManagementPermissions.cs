namespace Ejada.TaskManagement.Permissions;

public static class TaskManagementPermissions
{
    public const string GroupName = "TaskManagement";

    public static class Tasks
    {
        public const string Default = GroupName + ".Tasks";

        public const string Create = Default + ".Create";
        public const string Assign = Default + ".Assign";
        public const string ViewAll = Default + ".ViewAll";
        public const string ViewAssigned = Default + ".ViewAssigned";
        public const string StartTask = Default + ".StartTask";
        public const string FinishTask = Default + ".FinishTask";
        
    }

}
