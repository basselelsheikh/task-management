import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksComponent } from './tasks.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { CreateTaskComponent } from './create-task/create-task.component';

const routes: Routes = [
  {
    path: '', component: TasksComponent, pathMatch: 'full',

  },
  { path: 'create', component: CreateTaskComponent },
  { path: ':id', component: TaskDetailsComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
