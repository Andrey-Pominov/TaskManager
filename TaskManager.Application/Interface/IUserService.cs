using TaskManager.Domain.Entities;
using TaskManager.Shared.Common;

namespace TaskManager.Application.Interface;

public interface IUserService
{
    Task<Result<IQueryable<User>>> GetAllUserAsync(Guid userId);
}