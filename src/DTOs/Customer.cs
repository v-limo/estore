namespace Backend.DTOs;

public class CustomerDto
{
    [MaxLength(32)] public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
}