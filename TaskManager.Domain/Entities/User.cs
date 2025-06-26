namespace TaskManager.Domain.Entities;


public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "User";

    public ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
    public ICollection<Task> AssignedTasks { get; set; } = new List<Task>();
}
