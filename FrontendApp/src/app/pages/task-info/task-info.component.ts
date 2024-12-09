import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TaskDataDTO } from '../../models/dtos/task-data.dto';
import { TaskInfoDTO } from '../../models/dtos/task-info.dto';
import { TaskService } from '../../services/api-services/task.service';
import { UnionService } from '../../services/api-services/union.service';
import { ResourceType } from '../../enums/resource-type.enum';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
  styleUrls: ['./task-info.component.css']
})
export class TaskInfoComponent implements OnInit, OnDestroy {
  task: TaskDataDTO | null = null;
  companyRole: string | null = null;
  parentTask: TaskInfoDTO | null = null;
  resourceType = ResourceType;
  isNavigationOpen = false;
  isDetailsVisible = false;
  isSubtasksVisible = false;

  private navSubscription!: Subscription;

  constructor(
    private taskService: TaskService,
    private unionService: UnionService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const taskId = localStorage.getItem('taskId');
    this.companyRole = localStorage.getItem('companyRole');
    const companyId = localStorage.getItem('companyId');
    const parsedCompanyId = companyId ? parseInt(companyId, 10) : null;

    if (taskId) {
      this.taskService.getTaskData({
        username: localStorage.getItem('username'),
        companyId: parsedCompanyId,
        taskId: parseInt(taskId)
      }).subscribe(taskData => {
        this.task = taskData;
      });
    }
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  goToParentTask(): void {
    if (this.parentTask) {
      localStorage.setItem('taskId', this.parentTask.id.toString());
      this.router.navigate(['/task-info']);
    }
  }

  goToAllTasks(): void {
    this.router.navigate(['/all-tasks']);
  }

  showDetails(): void {
    this.isDetailsVisible = true;
    this.isSubtasksVisible = false;
  }

  showSubtasks(): void {
    this.isDetailsVisible = false;
    this.isSubtasksVisible = true;
  }
}
