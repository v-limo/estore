namespace EStoreAPI.Models;

public class Product : BaseClass
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<Review> Reviews { get; set; } = [];
}