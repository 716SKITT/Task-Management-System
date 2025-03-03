using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;


namespace TaskManager.Infrastructure;

public class TaskDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.Property(t => t.Title).HasMaxLength(100).IsRequired();
            entity.Property(t => t.Description).HasMaxLength(500);
            entity.Property(t => t.Status).HasConversion<string>().IsRequired();
        });
    }
}
