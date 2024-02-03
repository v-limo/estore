namespace Backend.Services.Implementations;

public class ProductService : CrudService<Product>, IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context) : base(context, context.Products)
    {
        _context = context;
    }


    public async Task<List<Product>> GetByNameAsync(string name)
    {
        try
        {
            return await _context.Products
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