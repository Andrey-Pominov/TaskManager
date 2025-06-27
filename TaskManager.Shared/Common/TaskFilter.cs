using TaskManager.Domain.Entities;

namespace TaskManager.Shared.Common;

public class TaskFilter
{
    public Status? Status { get; set; }
    public string? CreatedByUsername { get; set; }
}
