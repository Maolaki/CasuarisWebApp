import { Component, Input } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  @Input() currentPage = 'profile';
  isNavigationOpen = false;

  constructor(private navigationService: NavigationStateService) {
    this.navigationService.navigationOpen$.subscribe((state) => {
      this.isNavigationOpen = state;
    });
  }

  toggleNavigation(): void {
    this.navigationService.toggleNavigation(true);
  }
}
