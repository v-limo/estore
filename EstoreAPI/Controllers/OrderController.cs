namespace EStoreAPI.Controllers;

public class OrderController : CrudController<OrderDto, OrderCreateDto, OrderUpdateDto>

{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService) : base(orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<ActionResult<List<Order>>> GetByCustomerAsync(int customerId)
    {
        var orders = await _orderService.GetByCustomerAsync(customerId);
        return Ok(orders);
    }

    [HttpGet("product/{productId:int}")]
    public async Task<ActionResult<List<Order>>> GetByProductAsync(int productId)
    {
        var orders = await _orderService.GetByProductAsync(productId);
        return Ok(orders);
    }
}