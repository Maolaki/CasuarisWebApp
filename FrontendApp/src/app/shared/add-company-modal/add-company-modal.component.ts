import { Component, OnInit } from '@angular/core';
import { UnionService } from '../../services/api-services/union.service';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-add-company-modal',
  templateUrl: './add-company-modal.component.html',
  styleUrls: ['./add-company-modal.component.css', '../../../styles/modal.css'],
})
export class AddCompanyModalComponent implements OnInit {
  private modalId = 'add-company-modal';
  isVisible = false;
  company = { name: '', description: '' };
  selectedImageFile: File | null = null;

  constructor(
    private unionService: UnionService,
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
      this.selectedImageFile = input.files[0];
    }
  }

  addCompany(): void {
    const formData = new FormData();
    formData.append('username', localStorage.getItem('username') || '');
    formData.append('name', this.company.name);
    formData.append('description', this.company.description);
    if (this.selectedImageFile) {
      formData.append('imageFile', this.selectedImageFile, this.selectedImageFile.name);
    }

    this.unionService.addCompany(formData).subscribe(
      () => {
        this.modalService.closeModal(this.modalId);
      },
      (error) => {
        alert('Ошибка при добавлении компании: ' + error.message);
      }
    );
  }
}
