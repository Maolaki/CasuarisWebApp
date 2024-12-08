import { Component, Input } from '@angular/core';
import { InvitationDTO } from '../../models/dtos/invitation.dto';
import { InvitationType } from '../../enums/invitation-type.enum';

@Component({
  selector: 'app-invite-button',
  templateUrl: './invite-button.component.html',
  styleUrls: ['./invite-button.component.css']
})
export class InviteButtonComponent {
  @Input() invite!: InvitationDTO;
  invitationType = InvitationType;

  onView() {
    console.log(`View button clicked for Invite ID: ${this.invite.id}`);
  }

  onAccept() {
    console.log(`Accept button clicked for Invite ID: ${this.invite.id}`);
  }

  onDecline() {
    console.log(`Decline button clicked for Invite ID: ${this.invite.id}`);
  }
}
