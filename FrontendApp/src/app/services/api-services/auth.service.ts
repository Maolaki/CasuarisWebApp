import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterUserCommand } from '../../models/commands/authservice/register-user.command';
import { AuthenticatedDTO } from '../../models/dtos/authenticated.dto';
import { AuthenticateUserQuery } from '../../models/queries/authservice/authenticate-user.query';
import { RefreshTokenCommand } from '../../models/commands/authservice/refresh-token.command';
import { UserInfoDTO } from '../../models/dtos/user-info.dto';
import { GetUserInfoQuery } from '../../models/queries/authservice/get-user-info.query';
import { UpdateUserCommand } from '../../models/commands/authservice/update-user.command';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authApiUrl = 'https://localhost:7001/auth';
  private tokenApiUrl = 'https://localhost:7001/token';
  private userApiUrl = 'https://localhost:7001/user';

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

  refreshToken(command: RefreshTokenCommand): Observable<AuthenticatedDTO> {
    return this.http.post<AuthenticatedDTO>(`${this.tokenApiUrl}/refresh`, command);
  }

  revokeToken(refreshToken: string): Observable<void> {
    const headers = this.getAuthHeaders().set('Content-Type', 'application/json');
    return this.http.post<void>(`${this.tokenApiUrl}/revoke`, JSON.stringify(refreshToken), { headers });
  }

  revokeAllTokens(): Observable<void> {
    return this.http.post<void>(`${this.tokenApiUrl}/revoke-all`, {}, { headers: this.getAuthHeaders() });
  }

  getUserInfo(query: GetUserInfoQuery): Observable<UserInfoDTO> {
    return this.http.post<UserInfoDTO>(`${this.userApiUrl}/get-info`, query, { headers: this.getAuthHeaders() });
  }

  updateUser(command: UpdateUserCommand): Observable<void> {
    return this.http.post<void>(`${this.userApiUrl}/update-user`, command, { headers: this.getAuthHeaders() });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }
}
