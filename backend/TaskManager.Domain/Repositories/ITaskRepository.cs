using TaskManager.Domain.Entities;
namespace TaskManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync (int id);
        Task<List<TaskEntity>> GetTasksAsync(int page, int pageSize, Entities.TaskStatus? status);
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
        Task SaveChangesAsync();
    }
}
