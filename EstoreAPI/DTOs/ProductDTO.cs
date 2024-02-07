namespace EStoreAPI.DTOs;

public class ProductDto : IIdentifiable
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Id { get; set; }
}

public class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class ProductUpdateDto : IIdentifiable
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? CategoryId { get; set; }
    public int Id { get; set; }
}
