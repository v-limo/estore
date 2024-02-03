namespace Backend.Services.Implementations;

public class OrderService : CrudService<Order>, IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context) : base(context, context.Orders)
    {
        _context = context;
    }

    public async Task<List<Order>> GetByCustomerAsync(int customerId)
    {
        try
        {
            return await _context.Orders.Where(x => x.Customer != null && x.Customer.Id == customerId).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Order>> GetByProductAsync(int productId)
    {
        try
        {
            return await _context.Orders.Where(x => x.Products.Any(p => p.Id == productId)).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}