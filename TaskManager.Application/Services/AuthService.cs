using TaskManager.Application.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Shared.Common;

namespace TaskManager.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;


    public AuthService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Result<string>.Failure("Invalid username or password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email, user.Role.ToString());
        return Result<string>.Success(token);
    }

    public async Task<Result<User>> RegisterAsync(string username, string email, string password, Role role)
    {
        if (await _userRepository.ExistsByUsernameOrEmailAsync(username, email))
        {
            return Result<User>.Failure("Username or email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        await _userRepository.AddAsync(user);
        return Result<User>.Success(user);
    }
}