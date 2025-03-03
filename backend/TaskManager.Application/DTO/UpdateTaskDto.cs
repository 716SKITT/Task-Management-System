namespace TaskManager.Application.DTO;

public record UpdateTaskDto
(
    string Title,
    string Description,
    string Status
);

