namespace Backend.DTOs;

public class ProductDto
{
    [MaxLength(32)] public string Name { get; set; } = string.Empty;
}