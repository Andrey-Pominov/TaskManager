using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Shared.Common;

public class AssignTaskPayload
{
    public Task? Task { get; set; }
    public string? Error { get; set; }
}
