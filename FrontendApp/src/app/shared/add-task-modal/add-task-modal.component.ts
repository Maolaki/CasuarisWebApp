import { Component, OnInit } from '@angular/core';
import { AddTaskCommand } from '../../models/commands/taskservice/add-task.command';
import { TaskService } from '../../services/api-services/task.service';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-add-task-modal',
  templateUrl: './add-task-modal.component.html',
  styleUrls: ['./add-task-modal.component.css']
})
export class AddTaskModalComponent implements OnInit {
  private modalId = 'add-task-modal';
  isVisible = false;
  task = {
    name: '',
    description: '',
    budget: null,
    parentId: null as number | null
  };

  constructor(
    private taskService: TaskService,
    private modalService: ModalService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      if (state.data?.parentId !== undefined) {
        this.task.parentId = state.data.parentId;
      }
    });
  }

  openModal() {
    this.modalService.openModal(this.modalId);
  }

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }

  addTask() {
    const command: AddTaskCommand = {
      username: localStorage.getItem('username'),
      companyId: parseInt(localStorage.getItem('companyId') || '1', 10),
      parentId: this.task.parentId,
      name: this.task.name,
      description: this.task.description,
      budget: this.task.budget
    };

    this.taskService.addTask(command).subscribe(() => {
      location.reload();
      this.closeModal();
    }, (error) => {
      alert('Ошибка при добавлении задачи: ' + error.message);
    });
  }
}
