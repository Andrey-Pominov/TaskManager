namespace TaskManager.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Status { get; set; } = "Todo";
    public DateTime CreatedAt { get; set; }

    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = default!;

    public Guid? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
}