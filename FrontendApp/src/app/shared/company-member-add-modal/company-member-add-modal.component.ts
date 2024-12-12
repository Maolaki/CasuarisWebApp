import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UnionService } from '../../services/api-services/union.service';
import { ModalService } from '../../services/modal-service.service';
import { CompanyRole } from '../../enums/company-role.enum';
import { InvitationType } from '../../enums/invitation-type.enum';
import { AddInvitationCommand } from '../../models/commands/unionservice/add-invitation.command';

@Component({
  selector: 'app-company-member-add-modal',
  templateUrl: './company-member-add-modal.component.html',
  styleUrls: ['./company-member-add-modal.component.css', '../../../styles/modal.css']
})
export class CompanyMemberAddModalComponent implements OnInit {
  private modalId = 'company-member-add-modal';
  isVisible = false;
  CompanyRole = CompanyRole;

  addCommand: AddInvitationCommand = {
    username: null,
    companyId: null,
    memberUsername: null,
    description: null,
    role: CompanyRole.performer,
    teamId: null,
    type: InvitationType.company
  };

  constructor(
    private modalService: ModalService,
    private unionService: UnionService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
    });
  }

  closeModal(): void {
    this.modalService.closeModal(this.modalId);
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.addCommand.username = localStorage.getItem('username');
      this.addCommand.companyId = Number(localStorage.getItem('companyId')) || null;
      this.addCommand.type = InvitationType.company;

      this.unionService.addInvitation(this.addCommand).subscribe(() => {
        this.closeModal();
      }, error => {
        console.error('Ошибка при добавлении участника:', error);
      });
    }
  }
}
