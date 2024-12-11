import { Component, Input } from '@angular/core';
import { CompanyRole } from '../../enums/company-role.enum';
import { CompanyMemberDTO } from '../../models/dtos/company-member.dto';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-company-member',
  templateUrl: './company-member.component.html',
  styleUrl: './company-member.component.css'
})
export class CompanyMemberComponent {
  @Input() companyMember!: CompanyMemberDTO;
  @Input() currentUserRole!: CompanyRole;
  companyRole = CompanyRole;

  constructor(private modalService: ModalService) { }

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
    console.log(`Accept button clicked for Invite ID: ${this.companyMember.id}`);
  }

  onDelete() {
    console.log(`Decline button clicked for Invite ID: ${this.companyMember.id}`);
  }
}
