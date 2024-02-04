namespace Backend.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public Role Role { get; set; } = Role.User;
    public DateTime? RefreshTokenExpiryTime { get; set; }
}