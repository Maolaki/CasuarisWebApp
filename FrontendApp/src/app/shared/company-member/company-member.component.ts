import { Component, Input } from '@angular/core';
import { CompanyRole } from '../../enums/company-role.enum';
import { CompanyMemberDTO } from '../../models/dtos/company-member.dto';
import { ModalService } from '../../services/modal-service.service';
import { RemoveCompanyWorkerCommand } from '../../models/commands/unionservice/remove-company-worker.command';
import { UnionService } from '../../services/api-services/union.service';

@Component({
  selector: 'app-company-member',
  templateUrl: './company-member.component.html',
  styleUrl: './company-member.component.css'
})
export class CompanyMemberComponent {
  @Input() companyMember!: CompanyMemberDTO;
  @Input() currentUserRole!: CompanyRole;
  companyRole = CompanyRole;

  constructor(
    private modalService: ModalService,
    private unionService: UnionService
  ) { }

  getRoleName(role: CompanyRole): string {
    switch (role) {
      case CompanyRole.owner:
        return 'Owner';
      case CompanyRole.manager:
        return 'Manager';
      case CompanyRole.performer:
        return 'Performer';
      default:
        return 'Unknown Role';
    }
  }

  isManagerOrOwner(): boolean {
    return this.currentUserRole === CompanyRole.owner || this.currentUserRole === CompanyRole.manager;
  }

  onView(): void {
    this.modalService.openModal('company-member-info-modal', {
      companyMember: this.companyMember,
      currentUserRole: this.currentUserRole
    });
  }

  onChange() {
    this.modalService.openModal('company-member-update-modal', {
      memberId: this.companyMember.id,
      salary: this.companyMember.salary,
      workHours: this.companyMember.workHours,
      workDays: this.companyMember.workDays
    });
  }

  onDelete() {
    const command: RemoveCompanyWorkerCommand = {
      username: localStorage.getItem('username'),
      companyId: Number(localStorage.getItem('companyId')) || null,
      userId: this.companyMember.id,
      role: this.companyMember.companyRole
    };

    this.unionService.removeCompanyWorker(command).subscribe({
      next: () => {
        console.log('Company member successfully removed.');
        location.reload();
      },
      error: (err) => {
        console.error('Failed to remove company member:', err);
      }
    });
  }
}
