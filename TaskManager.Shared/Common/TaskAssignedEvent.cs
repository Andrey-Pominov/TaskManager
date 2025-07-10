namespace TaskManager.Shared.Common;

public class TaskAssignedEvent
{
    public Guid TaskId { get; set; }
    public Guid AssigneeUserId { get; set; }
    public string AssigneeEmail { get; set; }
    public string TaskTitle { get; set; }
}
