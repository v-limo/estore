namespace Backend.DTOs.AuthDTo;

public class LoginRequest
{
    [EmailAddress] public required string Email { get; set; }

    public required string Password { get; set; }
}