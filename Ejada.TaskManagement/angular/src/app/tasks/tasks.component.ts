import { AuthService, ListService, PagedResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetTaskListDto, TaskDto, TaskService } from '@proxy/tasks';


@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss',
  providers: [ListService]
})
export class TasksComponent implements OnInit {
  tasks = { items: [], totalCount: 0 } as PagedResultDto<TaskDto>


  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  constructor(private router: Router, private authService: AuthService, public readonly list: ListService, private taskService: TaskService, private permissionService: PermissionService) {
    if (!this.hasLoggedIn) {
      this.authService.navigateToLogin();
    }
  }
  ngOnInit(): void {

    if (this.permissionService.getGrantedPolicy('TaskManagement.Tasks.ViewAll')) {
      const taskStreamCreator = (query: GetTaskListDto) => this.taskService.getAllTasksByInput(query);

      this.list.hookToQuery(taskStreamCreator).subscribe((response) => {
        this.tasks = response;
      });
    }
    else if (this.permissionService.getGrantedPolicy('TaskManagement.Tasks.ViewAssigned')) {
      const taskStreamCreator = (query: GetTaskListDto) => this.taskService.getTasksAssignedToUser(query);

      this.list.hookToQuery(taskStreamCreator).subscribe((response) => {
        this.tasks = response;
      });
    }



  }

  login() {
    this.authService.navigateToLogin();
  }
}
