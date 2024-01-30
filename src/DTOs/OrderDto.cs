namespace Backend.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public Customer? Customer { get; set; }
    public List<ProductDto> Products { get; set; } = [];
}