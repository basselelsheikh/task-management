import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { TasksRoutingModule } from './tasks-routing.module';
import { TasksComponent } from './tasks.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CreateTaskComponent } from './create-task/create-task.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    TasksComponent,
    TaskDetailsComponent,
    CreateTaskComponent
  ],
  imports: [
    SharedModule,
    TasksRoutingModule,
    ThemeSharedModule,
    NgbDatepickerModule
  ]
})
export class TasksModule { }
