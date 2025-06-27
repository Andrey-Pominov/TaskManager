using HotChocolate.Authorization;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Queries;

public class RootQuery
{
    private readonly ILogger<RootQuery> _logger;

    public RootQuery(ILogger<RootQuery> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserTask> GetTasks([Service] ITaskService taskService)
    {
        _logger.LogInformation("Fetching all tasks");
        try
        {
            var result = taskService.GetAllTasksAsync();
            _logger.LogInformation("Successfully fetched all tasks");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching all tasks");
            throw;
        }
    }

    [UseProjection]
    public async Task<UserTask?> GetTaskById([Service] ITaskService taskService, Guid id)
    {
        _logger.LogInformation("Fetching task with id: {TaskId}", id);
        try
        {
            var result = await taskService.GetTaskByIdAsync(id);
            if (result == null)
            {
                _logger.LogWarning("Task not found for id: {TaskId}", id);
            }
            else
            {
                _logger.LogInformation("Successfully fetched task with id: {TaskId}", id);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching task with id: {TaskId}", id);
            throw;
        }
    }

    [Authorize(Roles = new[] { "Admin" })]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<User>> GetUsers([Service] IUserService userService,
        [GlobalState("userId")] Guid userId)
    {
        _logger.LogInformation("Fetching all users for admin userId: {UserId}", userId);
        try
        {
            var result = await userService.GetAllUserAsync(userId);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch users for admin userId: {UserId}. Error: {Error}", userId,
                    result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Successfully fetched all users for admin userId: {UserId}", userId);
            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching users for admin userId: {UserId}", userId);
            throw;
        }
    }
}