import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/api-services/auth.service';
import { NavigationStateService } from '../../services/navigation-state.service';
import { RegisterUserCommand } from '../../models/commands/authservice/register-user.command';
import { AuthenticateUserQuery } from '../../models/queries/authservice/authenticate-user.query';
import { AuthenticatedDTO } from '../../models/dtos/authenticated.dto';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit, OnDestroy {
  private navSubscription!: Subscription;
  isNavigationOpen = false;

  registerUser: RegisterUserCommand = {
    username: null,
    email: null,
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
    if (this.registerUser.username && this.registerUser.email && this.registerUser.password) {
      this.authService.registerUser(this.registerUser).subscribe({
        next: () => {
          const loginQuery: AuthenticateUserQuery = {
            login: this.registerUser.username,
            password: this.registerUser.password
          };

          this.authService.login(loginQuery).subscribe({
            next: (authData: AuthenticatedDTO) => {
              if (authData.accessToken && authData.refreshToken) {
                localStorage.setItem('accessToken', authData.accessToken);
                localStorage.setItem('refreshToken', authData.refreshToken);
                localStorage.setItem('username', this.registerUser.username!);

                this.router.navigate(['/home']);
              }
            },
            error: err => {
              console.error('Login failed:', err);
            }
          });
        },
        error: err => {
          console.error('Registration failed:', err);
        }
      });
    }
  }
}
