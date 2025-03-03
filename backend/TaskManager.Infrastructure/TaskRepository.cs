using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _dbContext;
        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TaskEntity?> GetByIdAsync(int id)
            => await _dbContext.Tasks.FindAsync(id);
        
        public async Task<List<TaskEntity>> GetTasksAsync(int page, int pageSize, Domain.Entities.TaskStatus? status)
        {
            var query = _dbContext.Tasks.AsQueryable();

            if(status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(TaskEntity task)
            => await _dbContext.Tasks.AddAsync(task);

        public async Task UpdateAsync(TaskEntity task)
            => await Task.Run(() => _dbContext.Tasks.Update(task));

        public async Task DeleteAsync(TaskEntity task)
            => await Task.Run(() => _dbContext.Tasks.Remove(task));

        public async Task SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
