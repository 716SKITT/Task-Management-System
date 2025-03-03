using TaskManager.Domain.Repositories;
using TaskManager.Application.DTO;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task CreateTaskAsync(CreateTaskDto dto)
    {
        var task = TaskEntity.Create(dto.Title, dto.Description);
        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(int id, UpdateTaskDto dto)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new Exception("not found");
        
        task.UpdateTitle(dto.Title);
        task.UpdateDescription(dto.Description);
        task.ChangeStatus(Enum.Parse<Domain.Entities.TaskStatus>(dto.Status));
        await _taskRepository.UpdateAsync(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task<List<TaskDto>> GetTasksAsync(int page, int pageSize, string? status)
    {
        Domain.Entities.TaskStatus? statusFilter = null;
        if(!string.IsNullOrEmpty(status))
        {
            statusFilter = Enum.Parse<Domain.Entities.TaskStatus>(status);
        }
        var tasks = await _taskRepository.GetTasksAsync(page, pageSize, statusFilter);
        return tasks.Select(t => new TaskDto(
            t.Id,
            t.Title,
            t.Description,
            t.Status.ToString(),
            t.CreatedAt,
            t.CompletedAt
        )).ToList();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException("Task not found");

        await _taskRepository.DeleteAsync(task);
        await _taskRepository.SaveChangesAsync();
    }
}

