namespace EStoreAPI.Models;

public class Category : BaseClass
{
    public string Name { get; set; } = null!;
    public IEnumerable<Product> Products { get; set; } = [];
}
