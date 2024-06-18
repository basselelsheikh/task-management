import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService, taskPriorityOptions, CreateTaskAttachmentDto, CreateTaskDto } from '@proxy/tasks';

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

  fileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const base64String = reader.result?.toString().split(',')[1];
        if (base64String) {
          resolve(base64String);
        } else {
          reject('Base64 string conversion failed');
        }
      };
      reader.onerror = (error) => {
        reject(error);
      };
    });
  }


  async save() {
    if (this.form.invalid) {
      return;
    }

    const attachments: CreateTaskAttachmentDto[] = [];
    for (const file of this.selectedFiles) {
      try {
        const base64String = await this.fileToBase64(file);
        const fileDto: CreateTaskAttachmentDto = {
          fileName: file.name,
          content: base64String
        };
        attachments.push(fileDto);
      } catch (error) {
        console.error('File conversion failed', error);
      }
    }

    const taskDto: CreateTaskDto = {
      ...this.form.value,
      attachments: attachments
    };

    this.taskService.createTask(taskDto).subscribe(() => {
      this.form.reset();
    });

  }



}
