using Ejada.TaskManagement.Samples;
using Xunit;

namespace Ejada.TaskManagement.EntityFrameworkCore.Domains;

[Collection(TaskManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TaskManagementEntityFrameworkCoreTestModule>
{

}
