namespace EStoreAPI.Services.Implementations;

public class AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    : IAuthService
{
    //TODO: use options

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                // Role = request.Role ?? Role.User //Todo: make a normal user.
                Role = Role.Admin // Todo
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new AuthResponse
                {
                    Success = false,
                    Message = result.Errors.Select(x => x.Description).ToList().ToString(),
                    Token = null,
                    RefreshToken = null
                };
            return new AuthResponse
            {
                Success = true,
                Message = "User created!!",
                Token = GenerateToken(user),
                RefreshToken = GenerateRefreshToken(user)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new AuthResponse
                {
                    Success = false,
                    Message = "User not found",
                    Token = null,
                    RefreshToken = null
                };
            var result = await userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid credentals",
                    Token = null,
                    RefreshToken = null
                };
            return new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = GenerateToken(user),
                RefreshToken = GenerateRefreshToken(user)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<AuthResponse> RefreshTokenAsync(string token)
    {
        try
        {
            await Task.Delay(1000);
            var user = await userManager.FindByEmailAsync(""); //TODO: get user
            return new AuthResponse
            {
                Success = true,
                Message = "Token refreshed",
                Token = GenerateToken(user!),
                RefreshToken = GenerateRefreshToken(user!)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<bool> RevokeTokenAsync(string token)
    {
        try
        {
            await Task.Delay(1000);
            return true; //TODO: implement
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserProfile?> GetUserProfileAsync(string? userId)
    {
        try
        {
            var appUser = await userManager.FindByIdAsync(userId ?? "");
            if (appUser == null) return null;
            return new UserProfile
            {
                Email = appUser.Email!,
                PhoneNumber = appUser.PhoneNumber
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string? GenerateToken(ApplicationUser user)
    {
        var validIssuer = configuration.GetSection("BearerAuthentication:ValidIssuer").Value!;
        var validAudiences = configuration.GetSection("BearerAuthentication:ValidAudiences").Value;
        var secret = configuration.GetSection("BearerAuthentication:Secret").Value!;

        if (string.IsNullOrEmpty(validIssuer) || string.IsNullOrEmpty(validAudiences) || string.IsNullOrEmpty(secret) ||
            user.Email == null)
            throw new Exception("Invalid configuration or user for token generation");


        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = validIssuer,
            Audience = validAudiences
        };

        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private static string GenerateRefreshToken(ApplicationUser user)
    {
        var refreshToken = new string(Random.Shared.ToString() + user.Role).Reverse().ToString() ?? string.Empty;
        return refreshToken;
    }
}