import { Component, OnInit, OnDestroy } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
})
export class NavigationComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  constructor(private navigationService: NavigationStateService) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
      this.updateNavigationState();
    });
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  closeNavigation(): void {
    this.navigationService.toggleNavigation(false);
  }

  private updateNavigationState(): void {
    const nav = document.querySelector('.navigation');
    if (nav) {
      if (this.isNavigationOpen) {
        nav.classList.add('open');
      } else {
        nav.classList.remove('open');
      }
    }
  }
}
