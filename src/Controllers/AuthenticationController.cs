namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly RoleManager<RoleManager<IdentityRole>> _roleManager;
    private readonly string _secret;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly List<string>? _validAudiences;

    private readonly string _validIssuer;

    public AuthenticationController(UserManager<IdentityUser> userManager,
        RoleManager<RoleManager<IdentityRole>> roleManager, string validIssuer, List<string>? validAudiences,
        string secret)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _validIssuer = validIssuer;
        _validAudiences = validAudiences;
        _secret = secret;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null) return Unauthorized();

        var validPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!validPassword) return Unauthorized();

        // var token = GenerateToken(user as Customer);
        const string token = "fake token        ";

        if (token is null) return Unauthorized();

        return Ok(new
        {
            token,
            Message = "login successfully"
        });
    }


    // [NonAction]
    // private string? GenerateToken(Customer? user)
    // {
    //     if (user is null) return null;
    //
    //     var credentials = new SigningCredentials(
    //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
    //         SecurityAlgorithms.HmacSha256Signature);
    //
    //     List<Claim> claims =
    //     [
    //         new Claim("Id", user.Id),
    //         new Claim(ClaimTypes.NameIdentifier, user.Id),
    //         new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
    //         new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
    //         new Claim(JwtRegisteredClaimNames.Name, user.Name),
    //         new Claim(JwtRegisteredClaimNames.Jti, user.Id),
    //         new Claim(ClaimTypes.Role, user.Role.ToString()),
    //         new Claim(JwtRegisteredClaimNames.NameId, user.Id)
    //     ];
    //
    //     var tokenDescriptor = new SecurityTokenDescriptor
    //     {
    //         Subject = new ClaimsIdentity(claims),
    //         Expires = DateTime.Now.AddHours(1),
    //         Issuer = _validIssuer,
    //         Audience = _validAudiences![0],
    //         IssuedAt = DateTime.Now,
    //         SigningCredentials = credentials
    //     };
    //
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //
    //     var token = tokenHandler.CreateToken(tokenDescriptor);
    //
    //     var stringToken = tokenHandler.WriteToken(token);
    //
    //     return stringToken;
    // }
    //
    //
    // [HttpPost("register")]
    // public async Task<IActionResult> Register(RegisterDto registerDto)
    // {
    //     var identityResult = await _userManager.CreateAsync(registerDto, registerDto.PassWord);
    //
    //     if (!identityResult.Succeeded)
    //         return BadRequest();
    //
    //     return Ok(identityResult);
    // }
    //
    // [HttpGet("profile")]
    // [Authorize(Policy = "profile")]
    // public async Task<IActionResult> Profile()
    // {
    //     if (!int.TryParse(ClaimTypes.NameIdentifier, out var id))
    //         return Unauthorized(new
    //         {
    //             message = $"Check the Id, Claim: {User.FindFirst(ClaimTypes.NameIdentifier)}, ID: {id} "
    //         });
    //
    //     var user = await _userManager.FindByIdAsync(id.ToString());
    //     if (user is null) return Unauthorized();
    //
    //     return Ok(user);
    // }
}