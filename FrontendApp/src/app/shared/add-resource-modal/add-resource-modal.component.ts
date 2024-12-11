import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../services/modal-service.service';
import { TaskService } from '../../services/api-services/task.service';
import { ResourceType } from '../../enums/resource-type.enum';

@Component({
  selector: 'app-add-resource-modal',
  templateUrl: './add-resource-modal.component.html',
  styleUrls: ['./add-resource-modal.component.css']
})
export class AddResourceModalComponent implements OnInit {
  private modalId = 'add-resource-modal';
  isVisible = false;
  resourceType = ResourceType;
  resource = {
    type: 0,
    resourceData: '',
    imageFile: null as File | null,
  };

  constructor(
    private taskService: TaskService,
    private modalService: ModalService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
    });
  }

  openModal() {
    this.modalService.openModal(this.modalId);
  }

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.resource.imageFile = input.files[0];
    }
  }

  addResource() {
    const formData = new FormData();
    formData.append('username', localStorage.getItem('username') || '');
    formData.append('companyId', localStorage.getItem('companyId') || '1');
    formData.append('taskInfoId', localStorage.getItem('taskId') || '1');
    formData.append('type', ResourceType[this.resource.type]);

    if (this.resource.type == 0) {
      formData.append('resourceData', this.resource.resourceData || '');
    } else {
      formData.append('imageFile', this.resource.imageFile!, this.resource.imageFile!.name);
    }

    this.taskService.addResource(formData).subscribe(() => {
      this.closeModal();
      location.reload();
    }, (error) => {
      alert('Ошибка при добавлении ресурса: ' + error.message);
    });
  }
}
