import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserInfoDTO } from '../../models/dtos/user-info.dto';
import { GetUserInfoQuery } from '../../models/queries/authservice/get-user-info.query';
import { AuthService } from '../../services/api-services/auth.service';
import { ModalService } from '../../services/modal-service.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css', '../../../styles/main-button.css', '../../../styles/main-text.css', '../../../styles/grid.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  private userSubscription!: Subscription;
  isNavigationOpen = false;
  userInfo: UserInfoDTO | null = null;

  constructor(
    private navigationService: NavigationStateService,
    private modalService: ModalService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });

    const username = localStorage.getItem('username');
    if (username) {
      const query: GetUserInfoQuery = { username };
      this.userSubscription = this.authService.getUserInfo(query).subscribe({
        next: (data) => {
          this.userInfo = data;
        },
        error: (err) => {
          console.error('Error fetching user info:', err);
        }
      });
    }
  }

  onChange() {
    this.modalService.openModal('profile-change-modal', {
      userInfo: this.userInfo,
    });
  }

  onRevokeTokens(): void {
    this.authService.revokeAllTokens().subscribe({
      next: () => {
        this.isNavigationOpen = false;
        this.router.navigate(['/home']);
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('username');
      },
      error: (err) => {
        console.error('Error revoking tokens:', err);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }
}
