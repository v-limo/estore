namespace EStoreAPI.Controllers;

public class ProductController(IProductService productService)
    : CrudController<ProductDto, ProductCreateDto, ProductUpdateDto>(productService)
{
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromQuery] string name)
    {
        return Ok(await productService.GetByNameAsync(name));
    }

    [HttpGet("/review{productId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<int>> GetReviewAverage(int productId)
    {
        return Ok(await productService.GetReviewAverageAsync(productId));
    }
}