import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService, taskPriorityOptions, CreateTaskDto } from '@proxy/tasks';

import { NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.scss'
})
export class CreateTaskComponent implements OnInit {
  @ViewChild('fileInput') fileInput: any;

  isLoading = false;
  form: FormGroup
  selectedFiles: File[] = [];
  employees = [];
  taskPriorities = taskPriorityOptions
  today: NgbDate;
  constructor(private fb: FormBuilder, private toaster: ToasterService, private calendar: NgbCalendar, private taskService: TaskService) { }
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
    this.isLoading = true;

    const formData = new FormData();

    for (const file of this.selectedFiles) {
      formData.append('attachments', file, file.name);
    }

    const createTaskDto: CreateTaskDto = {
      ...this.form.value,
      attachments: formData
    }

    createTaskDto.dueDate = new Date(this.form.value.dueDate).toISOString().split('T')[0];

    this.taskService.createTask(createTaskDto).subscribe(() => {
      this.form.reset();
      this.selectedFiles = [];
      const dataTransfer = new DataTransfer();
      this.fileInput.nativeElement.files = dataTransfer.files;
      this.isLoading = false;
      this.toaster.success('::TaskCreatedSuccessfully', '::Success');
    }, error => {
      this.isLoading = false;
    });

  }
}
