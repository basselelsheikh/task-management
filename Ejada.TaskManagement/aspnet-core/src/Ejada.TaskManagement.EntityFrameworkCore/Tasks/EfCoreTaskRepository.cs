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

            query = query
               .WhereIf(
                   !filter.IsNullOrEmpty(),
                   task => task.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase) ||
                   (task.Description != null && task.Description.Contains(filter!, StringComparison.OrdinalIgnoreCase))
                   );

            var count = await query.CountAsync();

            var tasksWithCreators = await query
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .Join(userDbSet,
                task => task.CreatorUserId,
                user => user.Id,
                (task, creatorUser) => new { Task = task, CreatorUser = creatorUser })
            .ToListAsync();

            var result = tasksWithCreators
                .GroupJoin(userDbSet,
                    taskWithCreator => taskWithCreator.Task.AssigneeUserId,
                    assigneeUser => assigneeUser.Id,
                    (taskWithCreator, assigneeUsers) => new { taskWithCreator.Task, taskWithCreator.CreatorUser, AssigneeUsers = assigneeUsers.DefaultIfEmpty() })
                .SelectMany(
                    taskWithCreatorAndAssignees => taskWithCreatorAndAssignees.AssigneeUsers.Select(assigneeUser => new EmployeeTaskViewModel
                    {
                        Task = taskWithCreatorAndAssignees.Task,
                        CreatorUserName = string.IsNullOrEmpty(taskWithCreatorAndAssignees.CreatorUser.Name) ? taskWithCreatorAndAssignees.CreatorUser.UserName : taskWithCreatorAndAssignees.CreatorUser.Name + " " + taskWithCreatorAndAssignees.CreatorUser.Surname,
                        AssigneeUserName = assigneeUser == null ? null : (string.IsNullOrEmpty(assigneeUser.Name) ? assigneeUser.UserName : assigneeUser.Name + " " + assigneeUser.Surname)
                    })
                )
                .ToList();

            return (result, count);

        }

    }
}