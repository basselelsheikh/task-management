using Ejada.TaskManagement.EntityFrameworkCore;
using Ejada.TaskManagement.Tasks.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Specifications;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Ejada.TaskManagement.Tasks
{
    public class EfCoreTaskRepository(
    IDbContextProvider<TaskManagementDbContext> dbContextProvider) : EfCoreRepository<TaskManagementDbContext, Task, Guid>(dbContextProvider),
        ITaskRepository
    {
        public async Task<EmployeeTaskViewModel> GetTaskWithDetails(Guid id)
        {
            var taskDbSet = await GetDbSetAsync();
            var userDbSet = (await GetDbContextAsync()).Users;

            var result = taskDbSet
                .Where(t => t.Id == id)
                .Include(t => t.Attachments)
                .Select(t => new EmployeeTaskViewModel
                {
                    Task = t,
                    CreatorUserName = userDbSet.Where(u => u.Id == t.CreatorUserId).Select(u => string.IsNullOrEmpty(u.Name) ? u.UserName : u.Name + " " + u.Surname).First(),
                    AssigneeUserName = userDbSet.Where(u => u.Id == t.AssigneeUserId).Select(u => string.IsNullOrEmpty(u.Name) ? u.UserName : u.Name + " " + u.Surname).First()
                }).First();
            return result;
        }
        public async Task<(List<EmployeeTaskViewModel> result, int count)> GetTasksAsync(int skipCount, int maxResultCount, string sorting, string? filter = null, ISpecification<Task>? spec = null)
        {
            var taskDbSet = await GetDbSetAsync();
            var userDbSet = (await GetDbContextAsync()).Users;

            IQueryable<Task> query = taskDbSet;

            if (spec != null)
            {
                query = query.Where(spec.ToExpression());
            }

            query = taskDbSet
               .WhereIf(
                   !filter.IsNullOrEmpty(),
                   task => task.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase) ||
                   (task.Description != null && task.Description.Contains(filter!, StringComparison.OrdinalIgnoreCase))
                   );

            var count = await query.CountAsync();

            var result = await query
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .Join(userDbSet,
                    t => t.CreatorUserId,
                    u => u.Id,
                    (task, user) => new { Task = task, CreatorUser = user })
                .Join(userDbSet,
                    joined => joined.Task.AssigneeUserId,
                    u => u.Id,
                    (joined, assigneeUser) => new EmployeeTaskViewModel
                    {
                        Task = joined.Task,
                        CreatorUserName = string.IsNullOrEmpty(joined.CreatorUser.Name) ? joined.CreatorUser.UserName : joined.CreatorUser.Name + " " + joined.CreatorUser.Surname,
                        AssigneeUserName = assigneeUser == null ? null : string.IsNullOrEmpty(assigneeUser.Name) ? assigneeUser.UserName : assigneeUser.Name + " " + assigneeUser.Surname
                    })
                .ToListAsync();

            return (result, count);

        }

    }
}