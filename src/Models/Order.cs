namespace Backend.Models;

public class Order : BaseModel
{
    public Customer Customer { get; set; } = new Customer();
    public List<Product> Products { get; set; } = [];
}