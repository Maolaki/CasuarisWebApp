import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddAccessPerformerCommand } from '../models/commands/unionservice/add-access-performer.command';
import { RemoveAccessPerformerCommand } from '../models/commands/unionservice/remove-access-performer.command';
import { AddCompanyCommand } from '../models/commands/unionservice/add-company.command';
import { UpdateCompanyCommand } from '../models/commands/unionservice/update-company.command';
import { RemoveCompanyCommand } from '../models/commands/unionservice/remove-company.command';
import { AddInvitationCommand } from '../models/commands/unionservice/add-invitation.command';
import { RemoveInvitationCommand } from '../models/commands/unionservice/remove-invitation.command';
import { AddTeamCommand } from '../models/commands/unionservice/add-team.command';
import { AddTeamMemberCommand } from '../models/commands/unionservice/add-team-member.command';
import { GetTeamsQuery } from '../models/queries/unionservice/get-teams.query';
import { RemoveTeamCommand } from '../models/commands/unionservice/remove-team.command';
import { RemoveTeamMemberCommand } from '../models/commands/unionservice/remove-team-member.command';
import { UpdateTeamCommand } from '../models/commands/unionservice/update-team.command';
import { TeamDTO } from '../models/dtos/team.dto';
import { GetCompaniesQuery } from '../models/queries/unionservice/get-companies.query';
import { RemoveCompanyWorkerCommand } from '../models/commands/unionservice/remove-company-worker.command';
import { AddCompanyWorkerCommand } from '../models/commands/unionservice/add-company-worker.command';
import { AddCompanyDateTimeCheckerCommand } from '../models/commands/unionservice/add-company-datetime-checker.command';
import { RemoveCompanyDateTimeCheckerCommand } from '../models/commands/unionservice/remove-company-datetime-checker.command';
import { CompanyDTO } from '../models/dtos/company.dto';

@Injectable({
  providedIn: 'root'
})
export class UnionService {
  private apiUrl = 'https://localhost:7002/unionservice';

  constructor(private http: HttpClient) { }

  // Access Operations
  addAccessPerformer(command: AddAccessPerformerCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/access/add-performer`, command, { headers: this.getAuthHeaders() });
  }

  removeAccessPerformer(command: RemoveAccessPerformerCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/access/remove-performer`, { body: command, headers: this.getAuthHeaders() });
  }

  // Company Operations
  addCompany(command: AddCompanyCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/company/add`, command, { headers: this.getAuthHeaders() });
  }

  updateCompany(command: UpdateCompanyCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/company/update`, command, { headers: this.getAuthHeaders() });
  }

  removeCompany(command: RemoveCompanyCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/company/remove`, { body: command, headers: this.getAuthHeaders() });
  }

  getCompanies(query: GetCompaniesQuery): Observable<CompanyDTO[]> {
    return this.http.post<CompanyDTO[]>(`${this.apiUrl}/company/get-companies`, query, { headers: this.getAuthHeaders() });
  }

  addCompanyWorker(command: AddCompanyWorkerCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/company/add-worker`, command, { headers: this.getAuthHeaders() });
  }

  removeCompanyWorker(command: RemoveCompanyWorkerCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/company/remove-worker`, { body: command, headers: this.getAuthHeaders() });
  }

  addCompanyDateTimeChecker(command: AddCompanyDateTimeCheckerCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/company/add-datetime-checker`, command, { headers: this.getAuthHeaders() });
  }

  removeCompanyDateTimeChecker(command: RemoveCompanyDateTimeCheckerCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/company/remove-datetime-checker`, { body: command, headers: this.getAuthHeaders() });
  }

  // Invitation Operations
  addInvitation(command: AddInvitationCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/invitation/add`, command, { headers: this.getAuthHeaders() });
  }

  removeInvitation(command: RemoveInvitationCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/invitation/remove`, { body: command, headers: this.getAuthHeaders() });
  }

  // Team Operations
  addTeam(command: AddTeamCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/team/add`, command, { headers: this.getAuthHeaders() });
  }

  addTeamMember(command: AddTeamMemberCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/team/addMember`, command, { headers: this.getAuthHeaders() });
  }

  getTeams(query: GetTeamsQuery): Observable<TeamDTO[]> {
    return this.http.post<TeamDTO[]>(`${this.apiUrl}/team/getTeams`, query, { headers: this.getAuthHeaders() });
  }

  removeTeam(command: RemoveTeamCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/team/remove`, { body: command, headers: this.getAuthHeaders() });
  }

  removeTeamMember(command: RemoveTeamMemberCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/team/removeMember`, { body: command, headers: this.getAuthHeaders() });
  }

  updateTeam(command: UpdateTeamCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/team/update`, command, { headers: this.getAuthHeaders() });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }
}
