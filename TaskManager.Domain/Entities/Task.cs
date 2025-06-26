using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    [MaxLength(200)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)] 
    [Required] 
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)] 
    public Status Status { get; set; } = Status.TODO;
    public DateTime CreatedAt { get; set; }

    public Guid CreatedById { get; set; }
    [Required] 
    public User CreatedBy { get; set; }

    public Guid? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
}