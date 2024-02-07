namespace Backend.Models;

public class Review : BaseClass
{
    public string Comment { get; set; } = null!;
    [Range(1, 5)] public int Rating { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}