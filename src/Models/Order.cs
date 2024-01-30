namespace Backend.Models;

public class Order
{
    [Key] public int Id { get; set; }
    public Customer? Customer { get; set; }
    public List<Product> Products { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}