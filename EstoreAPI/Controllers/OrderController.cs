// namespace EStoreAPI.Controllers;
//
// public class OrderController
//     : CrudController<OrderDto, OrderCreateDto, OrderUpdateDto>
// {
//     public OrderController(IOrderService orderService) : base(orderService)
//     {
//         _orderService = orderService;
//     }
//
//     private readonly IOrderService _orderService;
//
//     [HttpGet("customerss/{customerId:int}")]
//     public async Task<ActionResult<List<Order>>> GetByCustomerAsync(int customerId)
//     {
//         var orders = await _orderService.GetByCustomerAsync(customerId);
//         return Ok(orders);
//     }
//
//     [HttpGet("productss/{productId:int}")]
//     public async Task<ActionResult<List<Order>>> GetByProductAsync(int productId)
//     {
//         var orders = await _orderService.GetByProductAsync(productId);
//         return Ok(orders);
//     }
// }