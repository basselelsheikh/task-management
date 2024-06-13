using Xunit;

namespace Ejada.TaskManagement.EntityFrameworkCore;

[CollectionDefinition(TaskManagementTestConsts.CollectionDefinitionName)]
public class TaskManagementEntityFrameworkCoreCollection : ICollectionFixture<TaskManagementEntityFrameworkCoreFixture>
{

}
