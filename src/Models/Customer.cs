namespace Backend.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}