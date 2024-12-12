import { Component, OnInit } from '@angular/core';
import { ModalService } from '../../services/modal-service.service';
import { UpdateUserCommand } from '../../models/commands/authservice/update-user.command';
import { AuthService } from '../../services/api-services/auth.service';

@Component({
  selector: 'app-profile-change-modal',
  templateUrl: './profile-change-modal.component.html',
  styleUrls: ['./profile-change-modal.component.css', '../../../styles/modal.css']
})
export class ProfileChangeModalComponent implements OnInit {
  private modalId = 'profile-change-modal';
  isVisible = false;
  userInfo = { username: null, email: null, newUsername: null, newEmail: null };

  constructor(
    private modalService: ModalService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.modalService.modalState$(this.modalId).subscribe(state => {
      this.isVisible = state.isVisible;
      this.userInfo = state.data?.userInfo || { username: null, email: null, newUsername: null, newEmail: null };
    });
  }

  closeModal() {
    this.modalService.closeModal(this.modalId);
  }

  submitForm() {
    if (this.userInfo.newUsername || this.userInfo.newEmail) {
      const command: UpdateUserCommand = {
        username: localStorage.getItem('username'),
        newUsername: this.userInfo.newUsername,
        newEmail: this.userInfo.newEmail
      };

      this.authService.updateUser(command).subscribe({
        next: () => {
          if (this.userInfo.newUsername) {
            localStorage.setItem('username', this.userInfo.newUsername);
          }
          this.closeModal();
        },
        error: (error) => {
          console.error('Error updating user info:', error);
        }
      });
    }
  }
}
