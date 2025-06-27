using HotChocolate.Authorization;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Shared.Common;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Mutations;

[ExtendObjectType(typeof(RootMutation))]
public class TaskMutation
{
    [Authorize]
    public async Task<UserTask> CreateTask(
        [Service] ITaskService taskService,
        [GlobalState("userId")] Guid userId,
        string title,
        string description,
        Status status)
    {
        var result = await taskService.AddTaskAsync(userId, title, description, status);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }
        return result.Value;
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
        var result = await taskService.UpdateAsync(userId, taskId, title, description, status);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }
        return result.Value;
    }

    [Authorize(Roles = new[] { "Admin" })]
    public async Task<bool> DeleteTask(
        [Service] ITaskService taskService,
        [GlobalState("userId")] Guid userId,
        Guid taskId)
    {
        var result = await taskService.DeleteAsync(userId, taskId);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }
        return true;
    }

    [Authorize(Roles = new[] { "ADMIN" })]
    public async Task<AssignTaskPayload> AssignTaskToUser(
        [Service] ITaskService taskService,
        Guid taskId,
        Guid assignUserId)
    {
        var result = await taskService.AssignTaskToUserAsync(taskId, assignUserId);

        if (!result.IsSuccess)
        {
            return new AssignTaskPayload
            {
                Error = result.Error
            };
        }

        return new AssignTaskPayload
        {
            Task = result.Value
        };
    }

}
