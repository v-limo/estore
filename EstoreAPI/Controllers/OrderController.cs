namespace EStoreAPI.Controllers;

public class OrderController(IOrderService orderService)
    : CrudController<OrderDto, OrderCreateDto, OrderUpdateDto>(orderService)
{
    [HttpGet("customerss/{customerId:int}")]
    public async Task<ActionResult<List<Order>>> GetByCustomerAsync(int customerId)
    {
        var orders = await orderService.GetByCustomerAsync(customerId);
        return Ok(orders);
    }

    [HttpGet("productss/{productId:int}")]
    public async Task<ActionResult<List<Order>>> GetByProductAsync(int productId)
    {
        var orders = await orderService.GetByProductAsync(productId);
        return Ok(orders);
    }
}