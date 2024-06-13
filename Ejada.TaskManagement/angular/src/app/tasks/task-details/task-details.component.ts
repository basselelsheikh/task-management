import { PermissionService } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskDto, TaskService, TaskStatus, taskStatusOptions } from '@proxy/tasks';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.scss'
})
export class TaskDetailsComponent implements OnInit {
  task: TaskDto
  taskId: string
  statusOptions = []
  selectedStatus: TaskStatus
  TaskStatus = TaskStatus

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    public permissionService: PermissionService,
    private confirmation: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.taskId = this.route.snapshot.params['id'];
    this.taskService.getTask(this.taskId).subscribe(
      (response: TaskDto) => {
        this.task = response;
      }
    );

  }



  startTask() {
    this.confirmation
      .warn('::Confirmation:StartTask', '')
      .subscribe((status: Confirmation.Status) => {
        if (status === Confirmation.Status.confirm) {
          this.taskService.startTaskById(this.task.id).subscribe(
            (response: TaskStatus) => {
              this.task.status = response;
            }
          );
        }
      });
  }

  finishTask() {
    this.confirmation
      .warn('::Confirmation:FinishTask', '')
      .subscribe((status: Confirmation.Status) => {
        if (status === Confirmation.Status.confirm) {
          this.taskService.finishTaskById(this.task.id).subscribe(
            (response: TaskStatus) => {
              this.task.status = response;
            }
          );
        }
      });
  }

  downloadAttachment() {
    this.taskService.getAttachmentsById(this.task.id).subscribe( response => {
      const blob = response;
      const byteCharacters = atob(blob.content);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
      saveAs(new Blob([byteArray], { type: 'application/octet-stream' }), "Projects.docx");
    })
  }
}