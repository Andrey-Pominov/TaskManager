using HotChocolate.Authorization;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.MessageBroker.Interface;
using TaskManager.Shared.Common;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Mutations;

[ExtendObjectType(typeof(RootMutation))]
public class TaskMutation
{
    private readonly ILogger<TaskMutation> _logger;
    
    public TaskMutation(ILogger<TaskMutation> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public async Task<UserTask> CreateTask(
        [Service] ITaskService taskService,
        [GlobalState("userId")] Guid userId,
        string title,
        string description,
        Status status)
    {
        _logger.LogInformation("Creating task for userId: {UserId}, title: {Title}, status: {Status}", 
            userId, title, status);
        try
        {
            var result = await taskService.AddTaskAsync(userId, title, description, status);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Task creation failed for userId: {UserId}, title: {Title}. Error: {Error}", 
                    userId, title, result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Task created successfully for userId: {UserId}, taskId: {TaskId}", 
                userId, result.Value.Id);
            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during task creation for userId: {UserId}, title: {Title}", 
                userId, title);
            throw;
        }
    }

    [Authorize]
    public async Task<UserTask> UpdateTask(
        [Service] ITaskService taskService,
        [GlobalState("userId")] Guid userId,
        Guid taskId,
        string title,
        string description,
        Status status)
    {
        _logger.LogInformation("Updating task for userId: {UserId}, taskId: {TaskId}, title: {Title}, status: {Status}", 
            userId, taskId, title, status);
        try
        {
            var result = await taskService.UpdateAsync(userId, taskId, title, description, status);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Task update failed for userId: {UserId}, taskId: {TaskId}. Error: {Error}", 
                    userId, taskId, result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Task updated successfully for userId: {UserId}, taskId: {TaskId}", 
                userId, taskId);
            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during task update for userId: {UserId}, taskId: {TaskId}", 
                userId, taskId);
            throw;
        }
    }

    [Authorize(Roles = new[] { "Admin" })]
    public async Task<bool> DeleteTask(
        [Service] ITaskService taskService,
        [GlobalState("userId")] Guid userId,
        Guid taskId)
    {
        _logger.LogInformation("Deleting task for userId: {UserId}, taskId: {TaskId}", userId, taskId);
        try
        {
            var result = await taskService.DeleteAsync(userId, taskId);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Task deletion failed for userId: {UserId}, taskId: {TaskId}. Error: {Error}", 
                    userId, taskId, result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Task deleted successfully for userId: {UserId}, taskId: {TaskId}", 
                userId, taskId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during task deletion for userId: {UserId}, taskId: {TaskId}", 
                userId, taskId);
            throw;
        }
    }

    [Authorize(Roles = new[] { "Admin" })]
    public async Task<UserTask> AssignTaskToUser(
        [Service] ITaskService taskService,
        [Service] IMessagePublisher messagePublisher,
        Guid taskId,
        Guid assignUserId)
    {
        _logger.LogInformation("Assigning task taskId: {TaskId} to userId: {AssignUserId}", taskId, assignUserId);

        try
        {
            var result = await taskService.AssignTaskToUserAsync(taskId, assignUserId);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Task assignment failed for taskId: {TaskId}, assignUserId: {AssignUserId}. Error: {Error}",
                    taskId, assignUserId, result.Error);
                throw new GraphQLException(result.Error);
            }

            var task = result.Value;
            var eventMessage = new TaskAssignedEvent
            {
                TaskId = task.Id,
                AssigneeUserId = task.AssignedTo.Id,
                AssigneeEmail = task.AssignedTo.Email,
                TaskTitle = task.Title
            };

            await messagePublisher.PublishAsync("task.exchange", "task.assigned", eventMessage);

            _logger.LogInformation("Task assigned and event published: {TaskId}", task.Id);
            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during task assignment for taskId: {TaskId}, assignUserId: {AssignUserId}",
                taskId, assignUserId);
            throw;
        }
    }
}