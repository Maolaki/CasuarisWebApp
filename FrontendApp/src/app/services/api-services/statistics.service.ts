import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetCompanyStatisticsQuery } from '../../models/queries/statisticsservice/get-company-statistics.query';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  private apiUrl = 'https://localhost:7004/report';

  constructor(private http: HttpClient) { }

  getCompanyStatistics(query: GetCompanyStatisticsQuery): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/companyReport`, query, {
      headers: this.getAuthHeaders(),
    });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }
}
