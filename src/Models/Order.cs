namespace Backend.Models;

public class Order : BaseClass
{
    public int CustomerId { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; } = [];
}

public class OrderItem
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public int Quantity { get; set; }
}