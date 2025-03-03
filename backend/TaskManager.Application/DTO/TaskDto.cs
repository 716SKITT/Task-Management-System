namespace TaskManager.Application.DTO;

public record TaskDto
(
    int Id,
    string Title,
    string Description,
    string Status,
    DateTime CreatedAt,
    DateTime? CompletedAt
);

