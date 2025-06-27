using TaskManager.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(Guid id);
    Task<bool> ExistsByUsernameOrEmailAsync(string username, string email);
    Task AddAsync(User user);
    IQueryable<User> GetAll();
}