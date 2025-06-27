using TaskManager.Domain.Entities;
using TaskManager.Shared.Common;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interface;

public interface ITaskService
{
    IQueryable<UserTask> GetAllTasksAsync();
    Task<UserTask?> GetTaskByIdAsync(Guid taskId);
    Task<Result<UserTask>> AddTaskAsync(Guid userId, string title, string description, Status status);

    Task<Result<UserTask>> UpdateAsync(Guid userId, Guid taskId, string title, string description,
        Status status);
    Task<Result> DeleteAsync(Guid userId, Guid taskId);
    Task<Result<UserTask>> AssignTaskToUserAsync(Guid taskId, Guid userId);
}