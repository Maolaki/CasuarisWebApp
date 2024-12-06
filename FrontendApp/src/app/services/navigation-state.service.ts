import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NavigationStateService {
  private navigationOpenSubject = new BehaviorSubject<boolean>(false);
  navigationOpen$ = this.navigationOpenSubject.asObservable();

  toggleNavigation(state: boolean): void {
    this.navigationOpenSubject.next(state);
  }
}
