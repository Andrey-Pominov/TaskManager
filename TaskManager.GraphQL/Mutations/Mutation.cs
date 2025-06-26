using HotChocolate.Authorization;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.GraphQL.Mutations;

public class Mutation
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
        Role role = Role.USER)
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