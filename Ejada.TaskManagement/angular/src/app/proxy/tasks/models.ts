import type { TaskPriority } from './task-priority.enum';
import type { IRemoteStreamContent } from '../volo/abp/content/models';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { TaskStatus } from './task-status.enum';

export interface CreateTaskDto {
  name: string;
  description?: string;
  priority: TaskPriority;
  dueDate: string;
  employeeId?: string;
  attachments: FormData;
}

export interface EmployeeLookupDto extends EntityDto<string> {
  name?: string;
}

export interface GetTaskListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface TaskDto extends EntityDto<string> {
  name?: string;
  description?: string;
  dueDate?: string;
  priority: TaskPriority;
  status: TaskStatus;
  creatorUserId?: string;
  creatorUserName?: string;
  assigneeUserId?: string;
  assigneeUserName?: string;
  attachments: TaskDto_AttachmentDto[];
}

export interface TaskDto_AttachmentDto extends EntityDto<string> {
  blobName?: string;
}
