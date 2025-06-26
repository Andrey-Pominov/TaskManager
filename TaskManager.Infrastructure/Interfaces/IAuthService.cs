using TaskManager.Domain.Entities;
using TaskManager.Shared.Common;

namespace TaskManager.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<Result<string>> LoginAsync(string username, string password);
    Task<Result<User>> RegisterAsync(string username, string email, string password, Role role = Role.USER);
}