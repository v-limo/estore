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


    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<Product?>> GetProduct(Guid productId)
    {
        var product = await productService.GetByIdAsync(productId);
        return product;
    }


    [HttpPut("{productId:guid}")]
    public async Task<ActionResult<Product>> UpdateProduct(Guid productId, Product Product)
    {
        if (productId != Product.Id || !ModelState.IsValid)
            return BadRequest(
                new
                {
                    Message = "Product Id mismatch or invalid data",
                    ProductId = productId,
                    status = StatusCodes.Status400BadRequest
                }
            );

        var product = await productService.UpdateAsync(productId, Product);
        if (product is null)
            return NotFound(
                new { Message = "Product not found so cannot update", ProductId = productId }
            );
        return product;
    }


    [HttpDelete("{productId:guid}")]
    public async Task<ActionResult<bool>> DeleteProduct(Guid productId)
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