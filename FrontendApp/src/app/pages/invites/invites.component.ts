import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Subscription } from 'rxjs';
import { InvitationDTO } from '../../models/dtos/invitation.dto';
import { UnionService } from '../../services/api-services/union.service';
import { CompanyRole } from '../../enums/company-role.enum';
import { GetInvitationQuery } from '../../models/queries/unionservice/get-invitation.query';

@Component({
  selector: 'app-invites',
  templateUrl: './invites.component.html',
  styleUrls: ['./invites.component.css']
})
export class InvitesComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  invites: InvitationDTO[] = [];
  currentPage = 1;
  pageSize = 10;
  paginatedInvites: InvitationDTO[] = [];

  constructor(
    private navigationService: NavigationStateService,
    private unionService: UnionService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    this.updatePageSize();
    this.loadInvites();
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  loadInvites(): void {
    const query: GetInvitationQuery = {
      username: localStorage.getItem('username'),
      pageNumber: 1,
      pageSize: 1000
    };

    this.unionService.getInvitations(query).subscribe(
      (invitations) => {
        this.invites = invitations;
        this.paginate();
      },
      (error) => {
        console.error('Ошибка при загрузке приглашений:', error);
      }
    );
  }

  paginate(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedInvites = this.invites.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.paginate();
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.updatePageSize();
  }

  updatePageSize(): void {
    const width = window.innerWidth;
    if (width < 600) {
      this.pageSize = 10;
    } else if (width < 1200) {
      this.pageSize = 15;
    } else {
      this.pageSize = 20;
    }
    this.paginate();
  }

  getCompanyRoleString(role: CompanyRole): string {
    switch (role) {
      case CompanyRole.owner:
        return 'Owner';
      case CompanyRole.manager:
        return 'Manager';
      case CompanyRole.performer:
        return 'Performer';
      default:
        return 'Unknown';
    }
  }
}
