import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Subscription } from 'rxjs';
import { InvitationDTO } from '../../models/dtos/invitation.dto';
import { UnionService } from '../../services/api-services/union.service';
import { InvitationType } from '../../enums/invitation-type.enum';
import { CompanyRole } from '../../enums/company-role.enum';

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
    //const query = {
    //  username: null,
    //  pageNumber: this.currentPage,
    //  pageSize: this.pageSize
    //};

    //this.unionService.getInvitations(query).subscribe((invitations) => {
    //  this.invites = invitations;
    //  this.paginate();
    //});

    this.invites = Array.from({ length: 50 }, (_, i) => ({
      id: i + 1,
      description: `Invite to join Company ${i + 1}`,
      companyId: i + 1,
      companyRole: this.getRandomCompanyRole(),
      teamId: i % 3 === 0 ? i + 1 : null,
      type: i % 2 === 0 ? InvitationType.company : InvitationType.team
    }));

    this.paginate();
  }

  // Функция для генерации случайной роли компании для тестирования
  getRandomCompanyRole(): CompanyRole {
    const roles: CompanyRole[] = [CompanyRole.owner, CompanyRole.manager, CompanyRole.performer];
    return roles[Math.floor(Math.random() * roles.length)];
  }

  // Функция для генерации случайного типа приглашения для тестирования
  getRandomInvitationType(): InvitationType {
    const types: InvitationType[] = [InvitationType.company, InvitationType.team];
    return types[Math.floor(Math.random() * types.length)];
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
  // -----------------------------------------------------------

  paginate(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedInvites = this.invites.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.paginate();
    this.loadInvites();
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
}
