namespace EStoreAPI.DTOs;

public class AuthResponse
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public string? Token { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
}

public class LoginRequest
{
    [EmailAddress] public required string Email { get; init; } = null!;
    public required string Password { get; init; } = null!;
}

public class RegisterRequest
{
    [EmailAddress] public required string Email { get; init; } = null!;
    public required string Password { get; init; } = null!;
    public required string UserName { get; set; } = null!;
    public Role? Role { get; set; } = Models.Role.User;
}

public class UserProfile
{
    public string Email { get; set; } = null!;
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
}