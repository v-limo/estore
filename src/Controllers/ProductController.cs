namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>?> CreateProduct(Product? product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (product != null)
        {
            var createdProduct = await productService.CreateAsync(product);

            if (createdProduct != null) return CreatedAtAction(nameof(GetProduct), new { createdProduct.Id }, product);
        }

        return null;
    }


    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await productService.GetAllAsync();
    }


    [HttpGet("{productId:int}")]
    public async Task<ActionResult<Product?>> GetProduct(int productId)
    {
        var product = await productService.GetByIdAsync(productId);
        return product;
    }


    [HttpPut("{productId:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int productId, Product product)
    {
        if (productId != product.Id || !ModelState.IsValid)
            return BadRequest(
                new
                {
                    Message = "Product Id mismatch or invalid data",
                    ProductId = productId,
                    status = StatusCodes.Status400BadRequest
                }
            );

        var updateProduct = await productService.UpdateAsync(productId, product);
        if (updateProduct is null)
            return NotFound(
                new { Message = "Product not found so cannot update", ProductId = productId }
            );
        return product;
    }


    [HttpDelete("{productId:int}")]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        var result = await productService.DeleteAsync(productId);
        if (!result)
            return NotFound(
                new { Message = "Product not found so cannot delete", ProductId = productId }
            );
        // return result; ////alternativly
        return NoContent();
    }
}