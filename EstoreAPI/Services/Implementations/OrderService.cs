namespace EStoreAPI.Services.Implementations;

public class OrderService(ApplicationDbContext context, IMapper mapper)
    : CrudService<OrderDto, Order, OrderCreateDto, OrderUpdateDto>(context, mapper), IOrderService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;


    async Task<List<OrderDto>?> IOrderService.GetByProductAsync(int productId)
    {
        try
        {
            var orders = await _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.OrderItems.Any(y => y.ProductId == productId))
                .ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<List<OrderDto>> IOrderService.GetByCustomerAsync(int customerId)
    {
        try
        {
            var orders = await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}