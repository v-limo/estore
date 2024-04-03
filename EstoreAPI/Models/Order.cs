namespace EStoreAPI.Models;

public class Order : BaseClass
{
    public int CustomerId { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; } = [];
}

public class OrderItem : IIdentifiable
{
    public int? ProductId { get; set; }
    public int Quantity { get; set; }
    public int Id { get; set; }
}