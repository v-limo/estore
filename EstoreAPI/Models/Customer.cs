namespace EStoreAPI.Models;

public class Customer : BaseClass
{
    [MaxLength(25)] public string Name { get; set; } = null!;

    public IEnumerable<Order> Orders { get; set; } = null!;
}
