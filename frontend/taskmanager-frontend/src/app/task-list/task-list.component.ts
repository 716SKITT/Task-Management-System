import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { TaskDto, TaskStatus } from '../models/task';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  expandedTaskId: number | null = null;
  tasks: TaskDto[] = [];
  currentPage = 1;
  pageSize = 10;
  totalTasks = 0;
  selectedStatus?: TaskStatus;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  toggleDescription(taskId: number): void {
    this.expandedTaskId = this.expandedTaskId === taskId ? null : taskId;
  }

  isExpanded(taskId: number): boolean {
    return this.expandedTaskId === taskId;
  }
  
  loadTasks(): void {
    this.taskService.getTasks(this.currentPage, this.pageSize, this.selectedStatus)
      .subscribe(tasks => this.tasks = tasks);
  }

  onStatusChange(): void {
    this.currentPage = 1;
    this.loadTasks();
  }

  deleteTask(id: number): void {
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks();
      });
    
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadTasks();
    }
  }

  nextPage(): void {
    this.currentPage++;
    this.loadTasks();
  }
  getStatusText(status: string): string {
    const statusMap: {[key: string]: string} = {
      'New': 'Новая',
      'InProgress': 'В работе',
      'Completed': 'Завершена'
    };
    return statusMap[status] || status;
  }
}