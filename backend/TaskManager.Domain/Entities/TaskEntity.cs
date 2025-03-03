namespace TaskManager.Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; private set; }
        public string Title { get; private set; } 
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        private TaskEntity() { }

        public static TaskEntity Create(string title, string description)
        {
            return new TaskEntity
            {
                Title = title,
                Description = description,
                Status = TaskStatus.New,
                CreatedAt = DateTime.UtcNow
            };

        }
        
        public void UpdateTitle(string newTitle)
            => Title = newTitle;

        public void UpdateDescription(string newDescription)
            => Description = newDescription;

        public void ChangeStatus(TaskStatus newStatus)
        {
            if (newStatus == TaskStatus.Completed && Status != TaskStatus.Completed)
            {
                Status = newStatus;
                CompletedAt = DateTime.UtcNow;
            }
            else if(Status == TaskStatus.Completed && newStatus != TaskStatus.Completed)
            {
                Status = newStatus;
                CompletedAt = null;
            }
            else
            {
                Status = newStatus;
            }
        }
        public void Complete()
        {
            Status = TaskStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
    }


    public enum TaskStatus
    {
        New,
        InProgress,
        Completed
    }
}
