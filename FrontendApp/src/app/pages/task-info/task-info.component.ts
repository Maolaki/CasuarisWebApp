import { Component, OnDestroy, OnInit } from '@angular/core';
import { TaskStatus } from '../../enums/task-status.enum';
import { TaskDataDTO } from '../../models/dtos/task-data.dto';
import { ResourceType } from '../../enums/resource-type.enum';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
  styleUrls: ['./task-info.component.css']
})
export class TaskInfoComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  resourceType = ResourceType;

  constructor(
    private navigationService: NavigationStateService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  task: TaskDataDTO = {
    id: 1,
    companyId: null,
    name: 'Oreo Task',
    description: 'Oreo Task is a task management tool.',
    budget: 1000,
    status: TaskStatus.todo,
    completeDate: '2024-12-10',
    resources: [
      { id: 1, data: 'fewrhtyukilykhtrfewrhtyukilykhrt', imageFile: null, resourceType: ResourceType.text },
      { id: 2, data: 'efwhrgukjtefwukyhtrethukyjhtrukyikyhrthyjukjthrgthrgr', imageFile: null, resourceType: ResourceType.text }
    ],
    childTasks: [
      { id: 1, companyId: 3, name: 'Subtask 1', description: 'Subtask description', budget: 100, status: TaskStatus.inprogress, completeDate: '2024-12-10', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 64, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 77, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 7, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 5, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 42, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 3, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 31, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] },
      { id: 2, companyId: 4, name: 'Subtask 2', description: 'Subtask description', budget: 200, status: TaskStatus.done, completeDate: '2024-12-12', members: [] }
    ],
    members: []
  };

  isDetailsVisible = true;
  isSubtasksVisible = false;

  showDetails(): void {
    this.isDetailsVisible = true;
    this.isSubtasksVisible = false;
  }

  showSubtasks(): void {
    this.isDetailsVisible = false;
    this.isSubtasksVisible = true;
  }
}
