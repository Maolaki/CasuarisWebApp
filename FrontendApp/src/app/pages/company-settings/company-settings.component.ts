import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationStateService } from '../../services/navigation-state.service';
import { GetCompanyStatisticsQuery } from '../../models/queries/statisticsservice/get-company-statistics.query';
import { StatisticsService } from '../../services/api-services/statistics.service';

@Component({
  selector: 'app-company-settings',
  templateUrl: './company-settings.component.html',
  styleUrls: ['./company-settings.component.css', '../../../styles/main-button.css', '../../../styles/main-text.css']
})
export class CompanySettingsComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  constructor(
    private navigationService: NavigationStateService,
    private statisticsService: StatisticsService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  getCompanyReport(): void {
    const username = localStorage.getItem('username');
    const companyId = localStorage.getItem('companyId')
      ? Number(localStorage.getItem('companyId'))
      : null;

    const endDate = new Date();
    const startDate = new Date();
    startDate.setDate(endDate.getDate() - 14);

    const query: GetCompanyStatisticsQuery = {
      username,
      companyId,
      startDate,
      endDate
    };

    this.statisticsService.getCompanyStatistics(query).subscribe({
      next: () => {
        alert('Report request sent successfully!');
      },
      error: (err) => {
        console.error('Error fetching company statistics:', err);
        alert('Failed to fetch company statistics.');
      }
    });
  }
}
