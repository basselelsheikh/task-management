import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService, taskPriorityOptions, CreateTaskDto } from '@proxy/tasks';

import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.scss'
})
export class CreateTaskComponent implements OnInit {
  @ViewChild('fileInput') fileInput: any;

  form: FormGroup
  selectedFiles: File[] = [];
  employees = [];
  taskPriorities = taskPriorityOptions
  today: NgbDate;
  constructor(private fb: FormBuilder, private calendar: NgbCalendar, private taskService: TaskService) { }
  ngOnInit(): void {
    this.buildForm();
    this.today = this.calendar.getToday();
    this.taskService.getEmployeeLookup().subscribe((response) => {
      this.employees = response.items;
    })
  }


  buildForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      priority: [null, Validators.required],
      dueDate: ['', Validators.required],
      assignedTo: [''],
      employeeId: [null]
    })
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      for (let i = 0; i < input.files.length; i++) {
        this.selectedFiles.push(input.files[i]);
      }
    }
  }

  removeFile(index: number): void {
    this.selectedFiles.splice(index, 1);

    const dataTransfer = new DataTransfer();
    this.selectedFiles.forEach(file => dataTransfer.items.add(file));
    this.fileInput.nativeElement.files = dataTransfer.files;
  }

  async save() {
    if (this.form.invalid) {
      return;
    }

    const formData = new FormData();
    formData.append('name', this.form.get('name').value);
    formData.append('description', this.form.get('description')?.value);
    formData.append('priority', this.form.get('priority')!.value);
    formData.append('dueDate', new Date(this.form.get('dueDate').value).toISOString());
    formData.append('employeeId', this.form.get('employeeId')?.value);

    for (const file of this.selectedFiles) {
      formData.append('attachments', file, file.name);
    }

    this.taskService.createTask(formData).subscribe(() => {
      this.form.reset();
    });

  }
}
