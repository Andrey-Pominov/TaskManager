using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared.Common;

namespace TaskManager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IQueryable<User>>> GetAllUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null || user.Role != Role.Admin)
        {
            return Result<IQueryable<User>>.Failure("You are not allowed to get all users");
        }

        return Result<IQueryable<User>>.Success(_userRepository.GetAll());
    }
}