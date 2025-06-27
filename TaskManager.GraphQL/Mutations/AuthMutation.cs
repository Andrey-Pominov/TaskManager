using HotChocolate.Authorization;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;

namespace TaskManager.GraphQL.Mutations;

[ExtendObjectType(typeof(RootMutation))]
public class AuthMutation
{
    public async Task<string> Login([Service] IAuthService authService, string username, string password)
    {
        var result = await authService.LoginAsync(username, password);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }

        return result.Value;
    }

    public async Task<User> Register([Service] IAuthService authService, string username, string email, string password,
        Role role = Role.User)
    {
        var result = await authService.RegisterAsync(username, email, password, role);
        if (!result.IsSuccess)
        {
            throw new GraphQLException(result.Error);
        }

        return result.Value;
    }

    [Authorize]
    public string TestAuth([GlobalState("userId")] Guid userId, [GlobalState("userRole")] string userRole)
    {
        return $"Authenticated user: {userId}, Role: {userRole}";
    }
}