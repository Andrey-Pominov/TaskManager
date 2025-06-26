namespace TaskManager.Infrastructure.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string username, string email, string role);
}