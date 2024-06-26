﻿using Ejada.TaskManagement.Tasks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace Ejada.TaskManagement.Tasks
{
    public interface ITaskRepository : IBasicRepository<Task, Guid>
    {
        Task<(List<EmployeeTaskViewModel> result, int count)> GetTasksAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null,
            ISpecification<Task>? spec = null);

        Task<EmployeeTaskViewModel> GetTaskWithDetails(Guid id);
    }
}
