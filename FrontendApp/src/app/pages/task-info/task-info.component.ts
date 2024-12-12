import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { TaskDataDTO } from '../../models/dtos/task-data.dto';
import { TaskService } from '../../services/api-services/task.service';
import { ModalService } from '../../services/modal-service.service';
import { RemoveResourceCommand } from '../../models/commands/taskservice/remove-resource.command';
import { NavigationStateService } from '../../services/navigation-state.service';
import { RemoveTaskCommand } from '../../models/commands/taskservice/remove-task.command';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
  styleUrls: ['./task-info.component.css']
})
export class TaskInfoComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  task: TaskDataDTO | null = null;
  companyRole = 2;
  isDetailsVisible = true;
  isSubtasksVisible = false;
  private destroy$ = new Subject<void>();

  constructor(
    private navigationService: NavigationStateService,
    private taskService: TaskService,
    private router: Router,
    private modalService: ModalService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    const taskId = localStorage.getItem('taskId');
    const role = Number(localStorage.getItem('companyRole'));
    this.companyRole = role;
    const companyId = localStorage.getItem('companyId');
    const parsedCompanyId = companyId ? parseInt(companyId, 10) : null;

    if (taskId && parsedCompanyId) {
      this.taskService.getTaskData({
        username: localStorage.getItem('username'),
        companyId: parsedCompanyId,
        taskId: parseInt(taskId)
      })
        .pipe(takeUntil(this.destroy$))
        .subscribe(taskData => {
          this.task = taskData;
          this.processResources();
        });
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  processResources(): void {
    if (this.task?.resources) {
      this.task.resources.forEach(resource => {
        if (resource.type === 1 && resource.imageFile) {
          resource.imageFileUrl = this.createImageUrlFromFile(resource.imageFile);
        }
      });
    }
  }

  createImageUrlFromFile(imageFile: any): string {
    const file = new Blob([imageFile], { type: imageFile.contentType });
    return URL.createObjectURL(file);
  }

  goToParentTask(): void {
    const parentTaskId = this.task?.parentId;
    if (parentTaskId) {
      localStorage.setItem('taskId', parentTaskId.toString());
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

  openAddTaskModal(): void {
    const modalData = {
      parentId: this.task?.id || null,
    };
    this.modalService.openModal('add-task-modal', modalData);
  }

  openAddResourceModal(modalId: string): void {
    this.modalService.openModal(modalId);
  }

  openUpdateTaskModal(): void {
    this.modalService.openModal('task-update-modal', this.task);
  }

  removeTask() {
    const command: RemoveTaskCommand = {
      username: localStorage.getItem('username'),
      companyId: Number(localStorage.getItem('companyId')),
      taskId: this.task?.id || null
    };

    this.taskService.removeTask(command).subscribe(() => {
      this.router.navigate(['all-tasks']);
    });
  }

  removeResource(resourceId: number): void {
    const command: RemoveResourceCommand = {
      username: localStorage.getItem('username'),
      companyId: Number(localStorage.getItem('companyId')),
      resourceId: resourceId,
    };

    this.taskService.removeResource(command)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        () => {
          this.task!.resources = this.task!.resources!.filter(r => r.id !== resourceId);
        },
        (error) => {
          console.error('Ошибка при удалении ресурса:', error);
        }
      );
  }
}
