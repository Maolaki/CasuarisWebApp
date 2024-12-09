import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  styleUrls: ['./error-modal.component.css']
})
export class ErrorModalComponent {
  @Input() errorMessage = 'Произошла ошибка';
  @Output() close = new EventEmitter<void>();

  closeModal() {
    this.close.emit();
  }
}
