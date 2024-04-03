namespace EStoreAPI.Models;

public class Cart : BaseClass
{
    public int UserId { get; set; }
    public IEnumerable<CartItem>? CartItems { get; set; }
}

public class CartItem : IIdentifiable
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Id { get; set; }
}