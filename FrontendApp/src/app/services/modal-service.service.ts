import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  private errorSubject = new BehaviorSubject<string | null>(null);
  errorMessage$ = this.errorSubject.asObservable();

  setError(message: string): void {
    this.errorSubject.next(message);
  }

  clearError(): void {
    this.errorSubject.next(null);
  }

  private modalStates = new Map<string, BehaviorSubject<{ isVisible: boolean; data?: any }>>();

  openModal(modalId: string, data: any = null): void {
    const modalState = this.getModalState(modalId);
    modalState.next({ isVisible: true, data });
  }

  closeModal(modalId: string): void {
    const modalState = this.getModalState(modalId);
    modalState.next({ isVisible: false });
  }

  modalState$(modalId: string) {
    return this.getModalState(modalId).asObservable();
  }

  private getModalState(modalId: string): BehaviorSubject<{ isVisible: boolean; data?: any }> {
    if (!this.modalStates.has(modalId)) {
      this.modalStates.set(modalId, new BehaviorSubject<{ isVisible: boolean; data?: any }>({ isVisible: false }));
    }
    return this.modalStates.get(modalId)!;
  }
}
