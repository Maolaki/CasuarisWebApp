import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UnionService } from '../../services/api-services/union.service';

@Component({
  selector: 'app-add-company-modal',
  templateUrl: './add-company-modal.component.html',
  styleUrls: ['./add-company-modal.component.css']
})
export class AddCompanyModalComponent {
  @Input() isVisible = false;
  @Output() close = new EventEmitter<void>();
  company = { name: '', description: '', imageFile: null as File | null };

  constructor(private unionService: UnionService) { }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.[0]) {
      this.company.imageFile = input.files[0];
    }
  }

  addCompany(): void {
    const command = {
      userId: parseInt(localStorage.getItem('userId') || '', 10) || null,
      name: this.company.name,
      description: this.company.description,
      imageFile: this.company.imageFile
    };

    this.unionService.addCompany(command).subscribe(() => {
      alert('Компания успешно добавлена');
      this.closeModal();
    }, error => {
      alert('Ошибка при добавлении компании: ' + error.message);
    });
  }

  closeModal(): void {
    this.isVisible = false;
    this.close.emit();
  }
}
