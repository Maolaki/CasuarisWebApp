import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from './services/api-services/auth.service';
import { ModalService } from './services/modal-service.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private router: Router,
    private modalService: ModalService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          const accessToken = localStorage.getItem('accessToken');
          const refreshToken = localStorage.getItem('refreshToken');

          if (accessToken && refreshToken) {
            return this.authService.refreshToken({ accessToken, refreshToken }).pipe(
              switchMap(authData => {
                localStorage.setItem('accessToken', authData.accessToken ?? '');
                localStorage.setItem('username', authData.username ?? '');

                const clonedRequest = req.clone({
                  headers: req.headers.set('Authorization', `Bearer ${authData.accessToken}`),
                });
                return next.handle(clonedRequest);
              }),
              catchError(refreshError => {
                this.handleAuthError();
                return throwError(refreshError);
              })
            );
          } else {
            this.handleAuthError();
          }
        }

        return throwError(error);
      })
    );
  }

  private handleAuthError(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('username');
    this.router.navigate(['/home']).then(() => {
      this.modalService.setError('Произошла ошибка авторизации');
    });
  }
}
