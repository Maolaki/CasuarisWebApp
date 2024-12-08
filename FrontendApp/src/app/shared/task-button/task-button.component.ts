import { Component, Input } from '@angular/core';
import { TaskStatus } from '../../enums/task-status.enum';
import { TaskInfoDTO } from '../../models/dtos/task-info.dto';

@Component({
  selector: 'app-task-button',
  templateUrl: './task-button.component.html',
  styleUrl: './task-button.component.css'
})
export class TaskButtonComponent {
  @Input() task!: TaskInfoDTO;

  onTaskClick() {
    console.log(`Task button clicked for Task ID: ${this.task.id}`);
  }

  getTaskStatus(status: TaskStatus | null): string {
    switch (status) {
      case TaskStatus.todo:
        return 'To Do';
      case TaskStatus.inprogress:
        return 'In Progress';
      case TaskStatus.done:
        return 'Done';
      default:
        return 'Unknown';
    }
  }
}
