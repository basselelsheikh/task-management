import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { TaskPriority } from './task-priority.enum';
import type { TaskStatus } from './task-status.enum';

export interface AttachmentDto {
  content: string;
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
}
