

namespace Backend.Services.Implementations;

public class CustomerService : ICustomerService
{

    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> CreateAsync(Customer model)
    {
        _context.Customers.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return false;
        }
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Customer>> GetByNameAsync(string name)
    {
        return await _context.Customers.Where(x => x.Name.Contains(name.Trim())).ToListAsync();
    }

    public async Task<Customer?> UpdateAsync(Guid id, Customer model)
    {
        var foundCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (foundCustomer == null)
        {
            return null;
        }

        foundCustomer.Name = model.Name;
        foundCustomer.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return foundCustomer;
    }
}