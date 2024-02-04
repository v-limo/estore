namespace Backend.DTOs.AuthDTo;

public class RegisterRequest
{
    [EmailAddress] public required string Email { get; set; }

    public required string Password { get; set; }
    public required string UserName { get; set; }
}