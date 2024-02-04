namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _authService.LoginAsync(request);
        if (!response.Success) return Unauthorized(response.Message);
        return Ok(response);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _authService.RegisterAsync(request);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("profile")]
    [Authorize(Policy = "profile")]
    public async Task<IActionResult> Profile()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);


        if (userClaim is null) return NotFound("User not found");

        return Ok(userClaim.Value);
    }
}