namespace Backend.Services.Contracts;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(string token);
    Task<bool> RevokeTokenAsync(string token);
    Task<UserProfile?> GetUserProfileAsync(string? userId);
}