import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationStateService } from '../../services/navigation-state.service';
import { TaskService } from '../../services/api-services/task.service';
import { TaskStatus } from '../../enums/task-status.enum';
import { TaskInfoDTO } from '../../models/dtos/task-info.dto';
import { GetAllTasksInfoQuery } from '../../models/queries/taskservice/get-all-tasks-info.query';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-all-tasks',
  templateUrl: './all-tasks.component.html',
  styleUrls: ['./all-tasks.component.css']
})
export class AllTasksComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  tasks: TaskInfoDTO[] = [];
  filteredTasks: TaskInfoDTO[] = [];
  paginatedTasks: TaskInfoDTO[] = [];

  currentPage = 1;
  pageSize = 12;

  filter = {
    name: '',
    minBudget: null as number | null,
    maxBudget: null as number | null,
    status: ''
  };

  taskStatuses: string[] = Object.keys(TaskStatus)
    .filter(key => isNaN(Number(key)))
    .map(key => key);

  constructor(
    private navigationService: NavigationStateService,
    private taskService: TaskService,
    public modalService: ModalService 
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    this.updatePageSize();
    this.loadTasks();
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  loadTasks(): void {
    const companyId = localStorage.getItem('companyId');
    const parsedCompanyId = companyId ? parseInt(companyId, 10) : null;

    const query: GetAllTasksInfoQuery = {
      username: localStorage.getItem('username'),
      companyId: parsedCompanyId
    };

    this.taskService.getAllTasksInfo(query).subscribe(
      tasks => {
        this.tasks = tasks;
        this.filteredTasks = [...this.tasks];
        this.paginate();
      },
      error => {
        console.error('Ошибка при загрузке задач:', error);
      }
    );
  }

  applyFilters(): void {
    this.filteredTasks = this.tasks.filter(task => {
      const matchesName = !this.filter.name || task.name?.toLowerCase().includes(this.filter.name.toLowerCase());
      const matchesMinBudget = this.filter.minBudget === null || (task.budget !== null && task.budget >= this.filter.minBudget);
      const matchesMaxBudget = this.filter.maxBudget === null || (task.budget !== null && task.budget <= this.filter.maxBudget);

      const filterStatus = this.filter.status as unknown as TaskStatus | null;
      const matchesStatus = !filterStatus || task.status === filterStatus;

      return matchesName && matchesMinBudget && matchesMaxBudget && matchesStatus;
    });

    this.currentPage = 1;
    this.paginate();
  }

  paginate(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedTasks = this.filteredTasks.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.paginate();
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.updatePageSize();
  }

  updatePageSize(): void {
    const width = window.innerWidth;
    if (width < 600) {
      this.pageSize = 6;
    } else if (width < 1200) {
      this.pageSize = 9;
    } else {
      this.pageSize = 12;
    }
    this.paginate();
  }

  openAddTaskModal(modalId : string) {
    this.modalService.openModal(modalId);
  }

  closeModal(modalId: string) {
    this.modalService.closeModal(modalId);
  }
}
