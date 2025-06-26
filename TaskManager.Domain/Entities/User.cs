using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities;


public class User
{
    public Guid Id { get; set; }
    [MaxLength(100)] 
    [Required] 
    public string Username { get; set; } = string.Empty;
    [MaxLength(100)]
    [Required]
    public string Email { get; set; } = string.Empty;
    [MaxLength(256)]
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    [MaxLength(50)]
    [Required]
    public Role Role { get; set; } = Role.USER;

    public ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
    public ICollection<Task> AssignedTasks { get; set; } = new List<Task>();
}
