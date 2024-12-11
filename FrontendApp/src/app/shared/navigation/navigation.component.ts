import { Component, Input, OnInit, OnDestroy, SimpleChanges, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CompanyDTO } from '../../models/dtos/company.dto';
import { GetCompaniesQuery } from '../../models/queries/unionservice/get-companies.query';
import { UnionService } from '../../services/api-services/union.service';
import { NavigationStateService } from '../../services/navigation-state.service';
import { GetCompanyRoleQuery } from '../../models/queries/unionservice/get-company-role.query';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy, OnChanges {
  private navSubscription!: Subscription;
  private companiesSubscription!: Subscription;

  isNavigationOpen = false;
  isAddCompanyModalOpen = false;

  @Input() isAuthorized!: boolean;
  companies: CompanyDTO[] = [];

  constructor(
    private navigationService: NavigationStateService,
    private router: Router,
    private unionService: UnionService,
    private modalService: ModalService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
      this.updateNavigationState();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['isAuthorized'] && this.isAuthorized) {
      this.loadCompanies();
    }
  }

  ngOnDestroy(): void {
    this.navSubscription?.unsubscribe();
    this.companiesSubscription?.unsubscribe();
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

  navigateToHome(): void {
    this.router.navigate(['/home']);
  }

  loadCompanies(): void {
    const username = localStorage.getItem('username');
    if (!username) {
      console.error('User is not logged in');
      return;
    }

    const query: GetCompaniesQuery = {
      username,
      pageNumber: 1,
      pageSize: 1000
    };

    this.companiesSubscription = this.unionService.getCompanies(query).subscribe({
      next: (companies) => (this.companies = companies),
      error: (err) => console.error('Failed to load companies', err)
    });
  }

  selectCompany(company: CompanyDTO): void {
    localStorage.setItem('companyId', company.id.toString());

    const query: GetCompanyRoleQuery = {
      username: localStorage.getItem('username'),
      companyId: company.id
    };

    this.unionService.getCompanyRole(query).subscribe({
      next: (role) => {
        this.closeNavigation();
        localStorage.setItem('companyRole', role.toString());
        this.router.navigate(['/all-tasks']);
       // window.location.reload();
      },
      error: (err) => console.error('Failed to load company role', err)
    });
  }

  openAddCompanyModal(modalId: string) {
    this.modalService.openModal(modalId);
  }

  closeModal(modalId: string) {
    this.modalService.closeModal(modalId);
  }

  navigateToProfile(): void {
    this.closeNavigation();
    this.router.navigate(['/profile']);
  }

  navigateToInvites(): void {
    this.closeNavigation();
    this.router.navigate(['/invites']);
  }

  navigateToTeams(): void {
    this.closeNavigation();
    this.router.navigate(['/teams']);
  }
}
