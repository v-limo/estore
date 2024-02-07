namespace Backend.Models;

public class Cart : BaseClass
{
    public int UserId { get; set; }
    public IEnumerable<CartItem>? CartItems { get; set; }
}

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}