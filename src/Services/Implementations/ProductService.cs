

namespace Backend.Services.Implementations;

public class ProductService : IProductService
{
  private readonly ApplicationDbContext _context;

  public ProductService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Product?> CreateAsync(Product model)
  {
    var data = await _context.Products.AddAsync(model);
    return data.Entity;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
      return false;
    }
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<List<Product>> GetAllAsync()
  {
    return await _context.Products.ToListAsync();
  }

  public async Task<Product?> GetByIdAsync(Guid id)
  {
    return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<List<Product>> GetByNameAsync(string name)
  {
    return await _context.Products.Where(x => x.Name.Contains(name.Trim())).ToListAsync();
  }

  public async Task<Product?> UpdateAsync(Guid id, Product model)
  {
    var foundProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

    if (foundProduct == null)
    {
      return null;
    }

    foundProduct.Name = model.Name;
    foundProduct.UpdatedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync();
    return foundProduct;
  }
}
