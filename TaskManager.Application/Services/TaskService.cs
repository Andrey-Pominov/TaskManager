using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared.Common;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public IQueryable<UserTask> GetAllTasksAsync()
    {
        return _taskRepository.GetAllAsync();
    }


    public async Task<UserTask?> GetTaskByIdAsync(Guid taskId)
    {
        return await _taskRepository.GetByIdAsync(taskId);
    }

    public async Task<Result<UserTask>> AddTaskAsync(Guid userId, string title, string description, Status status)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result<UserTask>.Failure("User not found");
        }

        var newTask = new UserTask
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Status = status,
            CreatedAt = DateTime.UtcNow,
            CreatedById = userId,
            CreatedBy = user
        };

        var task = await _taskRepository.AddAsync(newTask);
        return Result<UserTask>.Success(task);
    }

    public async Task<Result<UserTask>> UpdateAsync(Guid userId, Guid taskId, string title, string description,
        Status status)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result<UserTask>.Failure("User not found");
        }

        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            return Result<UserTask>.Failure("Task not found");
        }

        if (task.CreatedById != userId && user.Role != Role.Admin)
        {
            return Result<UserTask>.Failure("You are not allowed to update this task");
        }

        task.Title = title;
        task.Description = description;
        task.Status = status;
        var updateTask = await _taskRepository.UpdateAsync(task);
        return Result<UserTask>.Success(updateTask);
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid taskId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result.Failure("User not found");
        }

        if (user.Role != Role.Admin)
        {
            return Result.Failure("You are not allowed to delete this task");
        }

        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            return Result.Failure("Task not found");
        }

        await _taskRepository.DeleteAsync(task);
        return Result.Success();
    }

    public async Task<Result<UserTask>> AssignTaskToUserAsync(Guid taskId, Guid userId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            return Result<UserTask>.Failure("Task not found");
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result<UserTask>.Failure("User not found");
        }

        task.AssignedToId = userId;
        task.AssignedTo = user;
        var updateTask = await _taskRepository.UpdateAsync(task);
        return Result<UserTask>.Success(updateTask);
    }
}
