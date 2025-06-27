using HotChocolate.Authorization;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;

namespace TaskManager.GraphQL.Mutations;

[ExtendObjectType(typeof(RootMutation))]
public class AuthMutation
{
    private readonly ILogger<AuthMutation> _logger;
    
    public AuthMutation(ILogger<AuthMutation> logger)
    {
        _logger = logger;
    }

    public async Task<string> Login([Service] IAuthService authService, string username, string password)
    {
        _logger.LogInformation("Attempting login for username: {Username}", username);
        try
        {
            var result = await authService.LoginAsync(username, password);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Login failed for username: {Username}. Error: {Error}", username, result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Login successful for username: {Username}", username);
            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for username: {Username}", username);
            throw;
        }
    }

    public async Task<User> Register([Service] IAuthService authService, string username, string email, string password,
        Role role = Role.User)
    {
        _logger.LogInformation("Attempting registration for username: {Username}, email: {Email}, role: {Role}", 
            username, email, role);
        try
        {
            var result = await authService.RegisterAsync(username, email, password, role);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Registration failed for username: {Username}. Error: {Error}", username, result.Error);
                throw new GraphQLException(result.Error);
            }

            _logger.LogInformation("Registration successful for username: {Username}", username);
            return result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during registration for username: {Username}", username);
            throw;
        }
    }

    [Authorize]
    public string TestAuth([GlobalState("userId")] Guid userId, [GlobalState("userRole")] string userRole)
    {
        _logger.LogInformation("TestAuth called for userId: {UserId}, role: {UserRole}", userId, userRole);
        return $"Authenticated user: {userId}, Role: {userRole}";
    }
}