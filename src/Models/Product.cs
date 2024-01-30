namespace Backend.Models;

public class Product
{
    public int Id { get; set; }
    [MaxLength(32)] public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}