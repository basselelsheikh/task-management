import type { AttachmentDto, GetTaskListDto, TaskDto } from './models';
import type { TaskStatus } from './task-status.enum';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  apiName = 'Default';
  

  finishTaskById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TaskStatus>({
      method: 'POST',
      url: `/api/app/task/${id}/finish-task`,
    },
    { apiName: this.apiName,...config });
  

  getAttachmentsById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AttachmentDto>({
      method: 'GET',
      url: `/api/app/task/${id}/attachments`,
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
