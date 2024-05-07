namespace EStoreAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]s[action]")]
[Authorize(Roles = "Admin")] // <- protect all routes by default
public class ApiControllerBase : ControllerBase;