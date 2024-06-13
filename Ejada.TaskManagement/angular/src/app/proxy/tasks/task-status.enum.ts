import { mapEnumToOptions } from '@abp/ng.core';

export enum TaskStatus {
  Pending = 0,
  InProgress = 1,
  Completed = 2,
}

export const taskStatusOptions = mapEnumToOptions(TaskStatus);
