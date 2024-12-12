import { Component, Input } from '@angular/core';
import { InvitationDTO } from '../../models/dtos/invitation.dto';
import { InvitationType } from '../../enums/invitation-type.enum';
import { CompanyRole } from '../../enums/company-role.enum';
import { UnionService } from '../../services/api-services/union.service';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-invite-button',
  templateUrl: './invite-button.component.html',
  styleUrls: ['./invite-button.component.css']
})
export class InviteButtonComponent {
  @Input() invite!: InvitationDTO;
  invitationType = InvitationType;

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

  onView(): void {
    this.modalService.openModal('invite-description-modal', {
      description: this.invite.description
    });
  }

  onAccept() {
    const command = this.createRemoveInvitationCommand(true);
    this.unionService.removeInvitation(command).subscribe({
      next: () => {
        console.log(`Invite ID: ${this.invite.id} accepted successfully.`);
      },
      error: (error) => {
        console.error(`Failed to accept Invite ID: ${this.invite.id}`, error);
      }
    });
  }

  onDecline() {
    const command = this.createRemoveInvitationCommand(false);
    this.unionService.removeInvitation(command).subscribe({
      next: () => {
        console.log(`Invite ID: ${this.invite.id} declined successfully.`);
      },
      error: (error) => {
        console.error(`Failed to decline Invite ID: ${this.invite.id}`, error);
      }
    });
  }

  private createRemoveInvitationCommand(answer: boolean): { username: string | null; invitationId: number | null; answer: boolean | null; } {
    return {
      username: localStorage.getItem('username'),
      invitationId: this.invite.id,
      answer: answer
    };
  }
}
