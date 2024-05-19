namespace EStoreAPI.Controllers;

public class OrderController(IOrderService orderService)
    : CrudController<OrderDto, OrderCreateDto, OrderUpdateDto>(orderService)
{
    [HttpGet("customer/{customerId:int}")]
    public async Task<ActionResult<List<Order>>> GetByCustomerAsync(int customerId)
    {
        var orders = await orderService.GetByProductAsync(customerId);
        return Ok(orders);
    }

  [HttpGet("products/{productId:int}")]
    public async Task<ActionResult<List<Order>>> GetByProductAsync(int productId)
    {
        var orders = await orderService.GetByProductAsync(productId);
        return Ok(orders);
    }
}