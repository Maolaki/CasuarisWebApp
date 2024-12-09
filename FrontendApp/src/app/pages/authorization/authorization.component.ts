import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticateUserQuery } from '../../models/queries/authservice/authenticate-user.query';
import { AuthService } from '../../services/api-services/auth.service';
import { NavigationStateService } from '../../services/navigation-state.service';
import { AuthenticatedDTO } from '../../models/dtos/authenticated.dto';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrl: './authorization.component.css'
})
export class AuthorizationComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  loginQuery: AuthenticateUserQuery = {
    login: null,
    password: null
  };

  constructor(
    private navigationService: NavigationStateService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.navSubscription = this.navigationService.navigationOpen$.subscribe(state => {
      this.isNavigationOpen = state;
    });
  }

  ngOnDestroy(): void {
    if (this.navSubscription) {
      this.navSubscription.unsubscribe();
    }
  }

  onSubmit(): void {
    this.authService.login(this.loginQuery).subscribe({
      next: (authData: AuthenticatedDTO) => {
        if (authData.username && authData.accessToken && authData.refreshToken) {
          localStorage.setItem('accessToken', authData.accessToken);
          localStorage.setItem('refreshToken', authData.refreshToken);
          localStorage.setItem('username', authData.username);

          this.router.navigate(['/home']);
        }
      },
      error: err => {
        console.error('Login failed:', err);
      }
    });
  }
}
