namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Order>?> CreateOrder(Order? order)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (order != null)
        {
            var createdOrder = await orderService.CreateAsync(order);

            if (createdOrder != null) return CreatedAtAction(nameof(GetOrder), new { createdOrder.Id }, order);
        }

        return null;
    }


    [HttpGet]
    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await orderService.GetAllAsync();
    }


    [HttpGet("{orderId:int}")]
    public async Task<ActionResult<Order?>> GetOrder(int orderId)
    {
        var order = await orderService.GetByIdAsync(orderId);
        return order;
    }


    [HttpPut("{orderId:int}")]
    public async Task<ActionResult<Order>> UpdateOrder(int orderId, Order order)
    {
        if (orderId != order.Id || !ModelState.IsValid)
            return BadRequest(
                new
                {
                    Message = "Order Id mismatch or invalid data",
                    OrderId = orderId,
                    status = StatusCodes.Status400BadRequest
                }
            );

        var updateAsync = await orderService.UpdateAsync(orderId, order);
        if (updateAsync is null)
            return NotFound(
                new { Message = "Order not found so cannot update", OrderId = orderId }
            );
        return updateAsync;
    }


    [HttpDelete("{orderId:int}")]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        var result = await orderService.DeleteAsync(orderId);
        if (!result)
            return NotFound(
                new { Message = "Order not found so cannot delete", OrderId = orderId }
            );
        return NoContent();
    }
}