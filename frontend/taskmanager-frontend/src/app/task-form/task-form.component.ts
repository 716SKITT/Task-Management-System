import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TaskService } from '../services/task.service';
import { CreateTaskDto, TaskDto, UpdateTaskDto, TaskFormModel } from '../models/task';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
  formModel: TaskFormModel = { title: '', description: '' };
  isEdit = false;
  currentId?: number;

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.isEdit = true;
      this.currentId = +id;
      this.loadTask(+id);
    }
  }

  loadTask(id: number): void {
    this.taskService.getTasks(1, 10)
      .subscribe(tasks => {
        const task = tasks.find(t => t.id === id);
        if (task) {
          this.formModel = {
            title: task.title,
            description: task.description,
            status: task.status
          };
        }
      });
  }

  onSubmit(): void {
    if (this.isEdit && this.currentId) {
      const updateData: UpdateTaskDto = {
        title: this.formModel.title || '',
        description: this.formModel.description || '',
        status: this.formModel.status || 'not_completed'
      };
      
      this.taskService.updateTask(this.currentId, updateData)
        .subscribe(() => this.router.navigate(['/']));
    } else {
      const createData: CreateTaskDto = {
        title: this.formModel.title || '',
        description: this.formModel.description || ''
      };
      
      this.taskService.createTask(createData)
        .subscribe(() => this.router.navigate(['/']));
    }
  }
}