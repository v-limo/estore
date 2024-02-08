namespace EStoreAPI.DTOs;

public class CustomerDto : IIdentifiable
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }
}

public class CustomerCreateDto
{
    public string Name { get; set; } = null!;
}

public class CustomerUpdateDto : IIdentifiable
{
    public string? Name { get; set; }
    public int Id { get; set; }
}
