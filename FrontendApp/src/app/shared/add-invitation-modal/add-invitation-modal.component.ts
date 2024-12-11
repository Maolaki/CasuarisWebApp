import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UnionService } from '../../services/api-services/union.service';
import { CompanyRole } from '../../enums/company-role.enum';
import { InvitationType } from '../../enums/invitation-type.enum';

@Component({
  selector: 'app-add-invitation-modal',
  templateUrl: './add-invitation-modal.component.html',
  styleUrls: ['./add-invitation-modal.component.css']
})
export class AddInvitationModalComponent {
  @Input() isVisible = false;
  @Input() invitationType: 'company' | 'team' = 'company';
  @Output() close = new EventEmitter<void>();

  invitation = {
    userId: null as number | null,
    description: '',
    role: null as CompanyRole | null,
    teamId: null as number | null
  };

  roles = Object.keys(CompanyRole).filter(key => isNaN(Number(key)));

  constructor(private unionService: UnionService) { }

  addInvitation(): void {
    const command = {
      username: localStorage.getItem('username'),
      description: this.invitation.description,
      userId: this.invitation.userId,
      companyId: parseInt(localStorage.getItem('companyId') || '', 10) || null,
      role: this.invitationType === 'company' ? this.invitation.role : null,
      teamId: this.invitationType === 'team' ? parseInt(localStorage.getItem('teamId') || '', 10) || null : null,
      type: this.invitationType === 'company' ? InvitationType.company : InvitationType.team
    };

    this.unionService.addInvitation(command).subscribe(() => {
      alert('Приглашение успешно добавлено');
      this.closeModal();
    }, error => {
      alert('Ошибка при добавлении приглашения: ' + error.message);
    });
  }

  closeModal(): void {
    this.isVisible = false;
    this.close.emit();
  }
}
