namespace EStoreAPI.DTOs;

public class CartDto : IIdentifiable
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}

public class CartCreateDto
{
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = [];
}

public class CartUpdateDto : IIdentifiable
{
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; } = [];
    public int Id { get; set; }
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}