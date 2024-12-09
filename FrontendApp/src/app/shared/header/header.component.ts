import { Component, Input, OnInit } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  @Input() currentPage!: string;
  @Input() isAuthorized!: boolean;
  isNavigationOpen = false;

  constructor(private navigationService: NavigationStateService) {
    this.navigationService.navigationOpen$.subscribe((state) => {
      this.isNavigationOpen = state;
    });
  }

  ngOnInit(): void {
    if (this.isAuthorized) {
      console.log("efwf");
    }
  }

  toggleNavigation(): void {
    this.navigationService.toggleNavigation(true);
  }
}
