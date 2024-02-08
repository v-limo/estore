namespace EStoreAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; } = null!;
    public Role Role { get; set; } = Role.User;
    public DateTime? RefreshTokenExpiryTime { get; set; }
}