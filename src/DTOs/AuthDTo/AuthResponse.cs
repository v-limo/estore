namespace Backend.DTOs.AuthDTo;

public class AuthResponse
{
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
    public string? Token { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
}