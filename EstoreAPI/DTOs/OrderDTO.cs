namespace EStoreAPI.DTOs;

public class OrderDto : IIdentifiable
{
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
    public int Id { get; set; }
}

public class OrderCreateDto
{
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
}

public class OrderUpdateDto : IIdentifiable
{
    public int CustomerId { get; set; }
    public int Id { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}