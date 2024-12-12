import { Component, Input, OnInit } from '@angular/core';
import { NavigationStateService } from '../../services/navigation-state.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/api-services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  @Input() currentPage!: string;
  @Input() isAuthorized!: boolean;
  @Input() companyRole: number | null = null;
  isNavigationOpen = false;
  currentTheme: 'light' | 'dark' = 'dark';

  constructor(
    private navigationService: NavigationStateService,
    private authService: AuthService,
    private router: Router
  ) {
    this.navigationService.navigationOpen$.subscribe((state) => {
      this.isNavigationOpen = state;
    });
  }

  ngOnInit(): void {
    if (this.isAuthorized) {
      console.log("efwf");
    }
  }

  toggleNavigation(): void {
    this.navigationService.toggleNavigation(true);
  }

  navigateTo(page: string): void {
    this.router.navigate([`/${page}`]);
  }

  logout(): void {
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) {
      this.clearSession();
      return;
    }

    this.authService.revokeToken(refreshToken).subscribe({
      next: () => {
        this.clearSession();
      },
      error: (err) => {
        console.error('Ошибка при отзыве токена:', err);
        this.clearSession();
      }
    });
  }

  private clearSession(): void {
    this.navigationService.toggleNavigation(false);
    this.router.navigate(['/home']);
    localStorage.removeItem('username');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  toggleTheme(): void {
    this.currentTheme = this.currentTheme === 'light' ? 'dark' : 'light';
    this.applyTheme(this.currentTheme);
    localStorage.setItem('theme', this.currentTheme);
  }

  private applyTheme(theme: 'light' | 'dark'): void {
    document.documentElement.setAttribute('data-theme', theme);
  }
}
