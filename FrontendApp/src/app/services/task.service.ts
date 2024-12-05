import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddResourceCommand } from '../models/commands/taskservice/add-resource.command';
import { AddTaskCommand } from '../models/commands/taskservice/add-task.command';
import { ChangeResourcePositionCommand } from '../models/commands/taskservice/change-resource-position.command';
import { RemoveResourceCommand } from '../models/commands/taskservice/remove-resource.command';
import { UpdateResourceCommand } from '../models/commands/taskservice/update-resource.command';
import { UpdateTaskCommand } from '../models/commands/taskservice/update-task.command';
import { TaskDataDTO } from '../models/dtos/task-data.dto';
import { TaskInfoDTO } from '../models/dtos/task-info.dto';
import { GetAllTasksInfoQuery } from '../models/queries/taskservice/get-all-tasks-info.query';
import { GetTaskDataQuery } from '../models/queries/taskservice/get-task-data.query';
import { RemoveTaskCommand } from '../models/commands/taskservice/remove-task.command';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'https://localhost:7001';

  constructor(private http: HttpClient) { }

  // Resource Methods
  addResource(command: AddResourceCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/resource/add`, command, { headers: this.getAuthHeaders() });
  }

  updateResource(command: UpdateResourceCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/resource/update`, command, { headers: this.getAuthHeaders() });
  }

  removeResource(command: RemoveResourceCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/resource/remove`, { body: command, headers: this.getAuthHeaders() });
  }

  changeResourcePosition(command: ChangeResourcePositionCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/resource/change-position`, command, { headers: this.getAuthHeaders() });
  }

  // Task Methods
  addTask(command: AddTaskCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/task/add`, command, { headers: this.getAuthHeaders() });
  }

  updateTask(command: UpdateTaskCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/task/update`, command, { headers: this.getAuthHeaders() });
  }

  removeTask(command: RemoveTaskCommand): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/task/remove`, { body: command, headers: this.getAuthHeaders() });
  }

  getAllTasksInfo(query: GetAllTasksInfoQuery): Observable<TaskInfoDTO[]> {
    return this.http.post<TaskInfoDTO[]>(`${this.apiUrl}/task/all`, query, { headers: this.getAuthHeaders() });
  }

  getTaskData(query: GetTaskDataQuery): Observable<TaskDataDTO> {
    return this.http.post<TaskDataDTO>(`${this.apiUrl}/task/data`, query, { headers: this.getAuthHeaders() });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }
}
