using Ejada.TaskManagement.Samples;
using Xunit;

namespace Ejada.TaskManagement.EntityFrameworkCore.Applications;

[Collection(TaskManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TaskManagementEntityFrameworkCoreTestModule>
{

}
