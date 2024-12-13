import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationStateService } from '../../services/navigation-state.service';
import { GetCompanyStatisticsQuery } from '../../models/queries/statisticsservice/get-company-statistics.query';
import { StatisticsService } from '../../services/api-services/statistics.service';
import { Router } from '@angular/router';
import { RemoveCompanyCommand } from '../../models/commands/unionservice/remove-company.command';
import { UnionService } from '../../services/api-services/union.service';
import { GetCompanyQuery } from '../../models/queries/unionservice/get-company.query';
import { CompanyDTO } from '../../models/dtos/company.dto';

@Component({
  selector: 'app-company-settings',
  templateUrl: './company-settings.component.html',
  styleUrls: ['./company-settings.component.css', '../../../styles/main-button.css', '../../../styles/main-text.css']
})
export class CompanySettingsComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  companyDTO: CompanyDTO | null = null;

  constructor(
    private navigationService: NavigationStateService,
    private statisticsService: StatisticsService,
    private unionService: UnionService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    const query: GetCompanyQuery = {
      username: localStorage.getItem('username'),
      companyId: Number(localStorage.getItem('companyId'))
    }

    this.unionService.getCompany(query).subscribe(
      answer => {
        this.companyDTO = answer;
      }
    )
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
      error: (err) => {
        console.error('Error fetching company statistics:', err);
      }
    });
  }

  removeCompany(): void {
    const username = localStorage.getItem('username');
    const companyId = Number(localStorage.getItem('companyId'))

    const command: RemoveCompanyCommand = { username, companyId };

    this.unionService.removeCompany(command).subscribe({
      next: () => {
        this.router.navigate(['/']);
        localStorage.removeItem('companyId');
      },
      error: (err) => {
        console.error('Error removing company:', err);
      }
    });
  }
}
