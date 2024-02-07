namespace Backend.DTOs;

public class CategoryDto : IIdentifiable
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }
}

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
}

public class CategoryUpdateDto : IIdentifiable
{
    public string? Name { get; set; }
    public int Id { get; set; }
}