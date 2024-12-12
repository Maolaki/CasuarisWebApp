import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/api-services/task.service';
import { ModalService } from '../../services/modal-service.service';
import { TaskStatus } from '../../enums/task-status.enum';
import { UpdateTaskCommand } from '../../models/commands/taskservice/update-task.command';
import { TaskDataDTO } from '../../models/dtos/task-data.dto';

@Component({
  selector: 'app-task-update-modal',
  templateUrl: './task-update-modal.component.html',
  styleUrls: ['./task-update-modal.component.css', '../../../styles/modal.css']
})
export class TaskUpdateModalComponent implements OnInit {
  private modalId = 'task-update-modal';
  isVisible = false;
  TaskStatus = TaskStatus;
  task: TaskDataDTO | null = null;
  userInfo = {
    name: '',
    description: '',
    budget: null as number | null,
    status: TaskStatus.todo
  };
  command: UpdateTaskCommand = {
    username: localStorage.getItem('username'),
    companyId: Number(localStorage.getItem('companyId')),
    taskId: null,
    name: null,
    description: null,
    budget: null,
    status: null
  };

  constructor(
    private modalService: ModalService,
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      this.task = state.data;
      if (this.task) {
        this.userInfo = {
          name: this.task.name || '',
          description: this.task.description || '',
          budget: this.task?.budget ?? null,
          status: this.task.status || TaskStatus.todo
        };
      }
    });
  }

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }

  submitForm() {
    if (this.userInfo.name || this.userInfo.description || this.userInfo.budget || this.userInfo.status) {

      this.command.taskId = this.task?.id || null;

      this.taskService.updateTask(this.command).subscribe(() => {
        this.closeModal();
      });
    }
  }
}
