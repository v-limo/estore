
namespace Backend.Services.Implementations;

public class OrderService : IOrderService
{

    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> CreateAsync(Order model)
    {
        var data = await _context.Orders.AddAsync(model);
        return data.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return false;
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<List<Order>> GetByCustomerAsync(Guid customerId)
    {

        return await _context.Orders.Where(x => x.Id == customerId).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetByProductAsync(Guid productId)
    {
        return await _context.Orders.Where(x => x.Id == productId).ToListAsync();
    }

    public async Task<Order?> UpdateAsync(Guid id, Order model)
    {
        var foundOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (foundOrder == null)
        {
            return null;
        }

        foundOrder.Customer = model.Customer;
        foundOrder.Products = model.Products;
        foundOrder.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return foundOrder;
    }
}