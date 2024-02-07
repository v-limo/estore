namespace EStoreAPI.Models;

public class BaseClass : IIdentifiable
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Id { get; set; }
}
