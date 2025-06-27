using HotChocolate.Authorization;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Queries;

public class Query
{
    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserTask> GetTasks([Service] ITaskService taskService)
    {
        return taskService.GetAllTasksAsync();
    }


    [UseProjection]
    public async Task<UserTask?> GetTaskById([Service] ITaskService taskService, Guid id)
    {
        return await taskService.GetTaskByIdAsync(id);
    }

    [Authorize(Roles = new[] { "Admin" })]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<User>> GetUsers([Service] IUserService userService,
        [GlobalState("userId")] Guid userId)
    {
        var result = await userService.GetAllUserAsync(userId);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }

        return result.Value;
    }
}