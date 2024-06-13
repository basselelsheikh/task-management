using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;

namespace Ejada.TaskManagement.Tasks.Specifications
{
    public class AssignedTasksToEmployeeSpecification(Guid employeeId) : Specification<Task>
    {

        public override Expression<Func<Task, bool>> ToExpression()
        {
            return t => t.AssigneeUserId == employeeId;
        }
    }
}
