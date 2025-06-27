using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Interfaces;
using UserTask = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserTask?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public IQueryable<UserTask> GetAllAsync()
    {
        return _context.Tasks.AsQueryable();
    }

    public async Task<UserTask> AddAsync(UserTask task)
    {
        var newTask = _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return newTask.Entity;
    }

    public async Task<UserTask> UpdateAsync(UserTask task)
    {
        var updateTask =  _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return updateTask.Entity;
    }

    public async Task DeleteAsync(UserTask task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}