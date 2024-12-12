import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationStateService } from '../../services/navigation-state.service';
import { UnionService } from '../../services/api-services/union.service';
import { CompanyMemberDTO } from '../../models/dtos/company-member.dto';
import { GetCompanyMembersQuery } from '../../models/queries/unionservice/get-company-members.dto';
import { CompanyRole } from '../../enums/company-role.enum';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-company-members',
  templateUrl: './company-members.component.html',
  styleUrls: ['./company-members.component.css', '../../../styles/grid.css']
})
export class CompanyMembersComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;
  companyMembers: CompanyMemberDTO[] = [];
  currentUserRole!: CompanyRole;

  constructor(
    private navigationService: NavigationStateService,
    private modalService: ModalService,
    private unionService: UnionService
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    const username = localStorage.getItem('username');
    const companyId = localStorage.getItem('companyId');
    const role = localStorage.getItem('companyRole');

    if (role) {
      this.currentUserRole = +role as CompanyRole;
    }

    if (username && companyId) {
      const query: GetCompanyMembersQuery = {
        username,
        companyId: parseInt(companyId, 10)
      };

      this.unionService.getCompanyMembers(query).subscribe({
        next: (members) => {
          this.companyMembers = members;
        },
        error: (err) => {
          console.error('Failed to load company members:', err);
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  openAddMemberModal(): void {
    this.modalService.openModal('company-member-add-modal');
  }
}
