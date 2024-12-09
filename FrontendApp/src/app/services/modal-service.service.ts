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
}
