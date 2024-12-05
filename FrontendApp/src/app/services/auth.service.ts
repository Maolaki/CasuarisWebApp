import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticatedDTO } from '../models/dtos/authenticated.dto';
import { AuthenticateUserQuery } from '../models/queries/authservice/authenticate-user.query';
import { RefreshTokensCommand } from '../models/commands/authservice/refresh-tokens.command';
import { RegisterUserCommand } from '../models/commands/authservice/register-user.command';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authApiUrl = 'https://localhost:7001/auth';
  private tokenApiUrl = 'https://localhost:7001/token';

  constructor(private http: HttpClient) { }

  registerUser(command: RegisterUserCommand): Observable<void> {
    return this.http.post<void>(`${this.authApiUrl}/register`, command);
  }

  login(query: AuthenticateUserQuery): Observable<AuthenticatedDTO> {
    return this.http.post<AuthenticatedDTO>(`${this.authApiUrl}/login`, query);
  }

  getUserId(): Observable<number> {
    return this.http.get<number>(`${this.authApiUrl}/get-id`, { headers: this.getAuthHeaders() });
  }

  refreshTokens(command: RefreshTokensCommand): Observable<AuthenticatedDTO> {
    return this.http.post<AuthenticatedDTO>(`${this.tokenApiUrl}/refresh`, command);
  }

  revokeToken(refreshToken: string): Observable<void> {
    return this.http.post<void>(`${this.tokenApiUrl}/revoke`, { refreshToken }, { headers: this.getAuthHeaders() });
  }

  revokeAllTokens(): Observable<void> {
    return this.http.post<void>(`${this.tokenApiUrl}/revoke-all`, {}, { headers: this.getAuthHeaders() });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }
}
