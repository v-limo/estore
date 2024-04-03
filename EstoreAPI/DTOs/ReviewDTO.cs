namespace EStoreAPI.DTOs;

public class ReviewDto : IIdentifiable
{
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Id { get; set; }
}

public class ReviewCreateDto
{
    public string Comment { get; set; } = string.Empty;
    [Range(1, 5)] public int Rating { get; set; }
    public int ProductId { get; set; }
    public int? UserId { get; set; }
}

public class ReviewUpdateDto : IIdentifiable
{
    public string Comment { get; set; } = string.Empty;
    [Range(1, 5)] public int Rating { get; set; }
    public int Id { get; set; }
}