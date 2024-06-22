import type { CreateTaskDto, EmployeeLookupDto, GetTaskListDto, TaskDto } from './models';
import type { TaskStatus } from './task-status.enum';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  apiName = 'Default';
  

  createTask = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/task/task',
      params: { name: input.name, description: input.description, priority: input.priority, dueDate: input.dueDate, employeeId: input.employeeId },
      body: input.attachments,
    },
    { apiName: this.apiName,...config });
  

  downloadAttachmentByBlobName = (blobName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'POST',
      responseType: 'blob',
      url: '/api/app/task/download-attachment',
      params: { blobName },
    },
    { apiName: this.apiName,...config });
  

  finishTaskById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskStatus>({
      method: 'POST',
      url: `/api/app/task/${id}/finish-task`,
    },
    { apiName: this.apiName,...config });
  

  getAllTasksByInput = (input: GetTaskListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TaskDto>>({
      method: 'GET',
      url: '/api/app/task/tasks',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getEmployeeLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EmployeeLookupDto>>({
      method: 'GET',
      url: '/api/app/task/employee-lookup',
    },
    { apiName: this.apiName,...config });
  

  getTask = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskDto>({
      method: 'GET',
      url: `/api/app/task/${id}/task`,
    },
    { apiName: this.apiName,...config });
  

  getTasksAssignedToUser = (input: GetTaskListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TaskDto>>({
      method: 'GET',
      url: '/api/app/task/tasks-assigned-to-user',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  startTaskById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskStatus>({
      method: 'POST',
      url: `/api/app/task/${id}/start-task`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
