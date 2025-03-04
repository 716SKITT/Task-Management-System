export type TaskStatus = 'New' | 'InProgress' | 'Completed';

export interface CreateTaskDto {
    title: string;
    description: string;
  }
  
  export interface TaskDto {
    id: number;
    title: string;
    description: string;
    status: string;
    createdAt: Date;
    completedAt: Date | null;
  }
  
  export interface UpdateTaskDto {
    title: string;
    description: string;
    status: string;
  }
  
  export type TaskFormModel = Partial<CreateTaskDto & UpdateTaskDto>;