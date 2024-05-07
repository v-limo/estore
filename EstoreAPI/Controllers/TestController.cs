using EStoreAPI.Data.Seeding;

namespace EStoreAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    private readonly DataSeed _dataSeed = new(1000);

    [HttpGet("products")]
    [AllowAnonymous]
    public IActionResult GetFakeProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        IEnumerable<Product> products = _dataSeed.SeedProduct();

        var totalPages = (int)Math.Ceiling((decimal)products.Count() / pageSize);
        Console.WriteLine(_dataSeed.SeedProduct());
        var response = new PaginatedResponse<Product>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            NextPage = pageNumber < totalPages ? pageNumber + 1 : null,
            TotalItems = products.Count(),
            Items = products.Skip((pageNumber - 1) * pageSize).Take(pageSize)
        };
        return Ok(response);
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public IActionResult GetFakeCategories()
    {
        IEnumerable<Category> categories = _dataSeed.SeedCategories();
        return Ok(categories);
    }
}