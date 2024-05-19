namespace EstoreAPI;

public class JwtUtilities
{
    private readonly string _validIssuer;
    private readonly List<string> _validAudiences;
    private readonly string _secret;

    public JwtUtilities(IConfiguration configuration)
    {
        _validIssuer = configuration.GetValue<string>("BearerAuthentication:ValidIssuer") ??
                       throw new ArgumentNullException("ValidIssuer is missing");
        _validAudiences = configuration.GetSection("BearerAuthentication:ValidAudiences").Get<List<string>>() ??
                          throw new ArgumentNullException("ValidAudiences is missing or invalid in configuration");
        _secret = configuration.GetValue<string>("BearerAuthentication:Secret") ??
                  throw new ArgumentNullException("Secret is missing in configuration");
    }

    public string GenerateToken(ApplicationUser user)
    {
        if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Id))
            throw new ArgumentException("User information is invalid for token generation");

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
            SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = _validIssuer,
            Audience = _validAudiences[0] // Just taking the first audience here for simplicity
        };

        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public TokenValidationParameters TokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _validIssuer,
            ValidAudiences = _validAudiences,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret))
        };
    }
}