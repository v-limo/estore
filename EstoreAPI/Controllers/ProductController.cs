namespace EStoreAPI.Controllers;

public class ProductController : CrudController<ProductDto, ProductCreateDto, ProductUpdateDto>
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) : base(productService)
    {
        _productService = productService;
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromQuery] string name)
    {
        return Ok(await _productService.GetByNameAsync(name));
    }

    [HttpGet("/review{productId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<int>> GetReviewAverage(int productId)
    {
        return Ok(await _productService.GetReviewAverageAsync(productId));
    }
}