import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { TaskStatus } from '../../enums/task-status.enum';
import { TaskInfoDTO } from '../../models/dtos/task-info.dto';
import { TaskService } from '../../services/api-services/task.service';
import { UnionService } from '../../services/api-services/union.service';

@Component({
  selector: 'app-task-button',
  templateUrl: './task-button.component.html',
  styleUrls: ['./task-button.component.css']
})
export class TaskButtonComponent {
  @Input() task!: TaskInfoDTO;

  constructor(
    private router: Router,
    private taskService: TaskService,
    private unionService: UnionService
  ) { }

  onTaskClick() {
    localStorage.setItem('taskId', this.task.id.toString());

    this.taskService.getTaskData({
      username: localStorage.getItem('username'),
      companyId: this.task.companyId,
      taskId: this.task.id
    }).subscribe(taskData => {
      localStorage.setItem('task', JSON.stringify(taskData));

      this.router.navigate(['/task-info']);
    });
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
