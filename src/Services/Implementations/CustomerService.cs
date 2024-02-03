namespace Backend.Services.Implementations;

public class CustomerService : CrudService<Customer>, ICustomerService
{
    private readonly ApplicationDbContext _context;


    public CustomerService(ApplicationDbContext context) : base(context, context.Customers)
    {
        _context = context;
    }


    public async Task<List<Customer>> GetByNameAsync(string name)
    {
        try
        {
            return await _context.Customers
                .Where(x => x.Name.Contains(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}