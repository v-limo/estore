namespace Backend.DTOs;

public class RegisterDto
{
    public required string Email { get; set; }
    public required string PassWord { get; set; }
    public required string UserName { get; set; }
}