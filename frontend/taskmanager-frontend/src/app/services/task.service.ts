import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTaskDto, TaskDto, UpdateTaskDto, TaskStatus } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5000/api/tasks';

  constructor(private http: HttpClient) { }

  getTasks(page: number, pageSize: number, status?: TaskStatus): Observable<TaskDto[]> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (status) params = params.set('status', status);

    return this.http.get<TaskDto[]>(this.apiUrl, { params });
  }

  createTask(task: CreateTaskDto): Observable<any> {
    return this.http.post(this.apiUrl, task);
  }

  updateTask(id: number, task: UpdateTaskDto): Observable<any> {
    if (task.status === 'Completed') {
      const completedTask = {...task, completedAt: new Date().toISOString()};
      return this.http.put(`${this.apiUrl}/${id}`, completedTask);
    }
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }

  deleteTask(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}