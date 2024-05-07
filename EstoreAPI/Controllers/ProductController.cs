namespace EStoreAPI.Controllers;

public class ProductController(IProductService productService)
    : CrudController<ProductDto, ProductCreateDto, ProductUpdateDto>(productService)
{
    [HttpGet]
    [Route("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromQuery] string name)
    {
        return Ok(await productService.GetByNameAsync(name));
    }

    [HttpGet]
    [Route("/review{productId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<int>> GetReviewAverage(int productId)
    {
        return Ok(await productService.GetReviewAverageAsync(productId));
    }
}