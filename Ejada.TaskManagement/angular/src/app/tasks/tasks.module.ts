import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { TasksRoutingModule } from './tasks-routing.module';
import { TasksComponent } from './tasks.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { ThemeSharedModule } from '@abp/ng.theme.shared';


@NgModule({
  declarations: [
    TasksComponent,
    TaskDetailsComponent
  ],
  imports: [
    SharedModule,
    TasksRoutingModule,
    ThemeSharedModule
  ]
})
export class TasksModule { }
