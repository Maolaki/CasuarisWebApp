import { Component, OnInit } from '@angular/core';
import { CompanyRole } from '../../enums/company-role.enum';
import { CompanyMemberDTO } from '../../models/dtos/company-member.dto';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-company-member-info-modal',
  templateUrl: './company-member-info-modal.component.html',
  styleUrl: './company-member-info-modal.component.css'
})
export class CompanyMemberInfoModalComponent implements OnInit {
  private modalId = 'company-member-info-modal';
  isVisible = false;
  companyMember: CompanyMemberDTO | null = null;
  currentUserRole: CompanyRole | null = null;
  companyRole = CompanyRole;

  constructor(private modalService: ModalService) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      this.companyMember = state.data?.companyMember || null;
      this.currentUserRole = state.data?.currentUserRole || null;
    });
  }

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

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }
}
