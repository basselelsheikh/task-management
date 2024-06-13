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

            var result = taskDbSet.Where(t => t.Id == id)
                .Select(t => new EmployeeTaskViewModel
                {
                    Task = t,
                    CreatorUserName = userDbSet.Where(u => u.Id == t.CreatorUserId).Select(u => string.IsNullOrEmpty(u.Name) ? u.UserName : u.Name + " " + u.Surname).First(),
                    AssigneeUserName = userDbSet.Where(u => u.Id == t.AssigneeUserId).Select(u => string.IsNullOrEmpty(u.Name) ? u.UserName : u.Name + " " + u.Surname).First()
                }).First();
            return result;
        }
        public async Task<(List<EmployeeTaskViewModel> result, int count)> GetTasksAsync(ISpecification<Task> spec, int skipCount, int maxResultCount, string sorting, string? filter = null)
        {
            var taskDbSet = await GetDbSetAsync();
            var userDbSet = (await GetDbContextAsync()).Users;

            var query = taskDbSet
                .Where(spec.ToExpression())
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
                    (task, user) => new EmployeeTaskViewModel
                    {
                        Task = task,
                        CreatorUserName = string.IsNullOrEmpty(user.Name) ? user.UserName : user.Name + " " + user.Surname,
                    })
                .ToListAsync();

            return (result, count);

        }

    }
}