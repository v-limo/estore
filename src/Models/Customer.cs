namespace Backend.Models;

public class Customer
{
    [MaxLength(32)] public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public Role Role { get; set; } = Role.User;
    public IEnumerable<Order> Orders { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}