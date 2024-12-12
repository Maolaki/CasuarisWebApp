import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationStateService } from '../../services/navigation-state.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', '../../../styles/grid.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  constructor(
    private navigationService: NavigationStateService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });
  }

    ngOnDestroy(): void {
      if(this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }
}
