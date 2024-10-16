namespace EStoreAPI.Controllers;

public class AuthController(IAuthService authService) : ApiControllerBase
{
    [HttpGet("profile")]
    [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> Profile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null) return Unauthorized();

        var user = await authService.GetUserProfileAsync(userIdClaim.Value);

        if (user is null) return Unauthorized();

        return Ok(user);
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await authService.LoginAsync(request);
        if (!response.Success) return Unauthorized(response.Message);
        return Ok(response);
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await authService.RegisterAsync(request);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }
}