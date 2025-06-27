namespace TaskManager.Infrastructure.Interfaces;

public interface ITaskRepository
{
    Task<Domain.Entities.Task?> GetByIdAsync(Guid id);
    IQueryable<Domain.Entities.Task> GetAllAsync();
    // Task<List<Domain.Entities.Task>> GetByFilterAsync(string filter);
    Task<Domain.Entities.Task> AddAsync(Domain.Entities.Task task);
    Task<Domain.Entities.Task> UpdateAsync(Domain.Entities.Task task);
    Task DeleteAsync(Domain.Entities.Task task);
}