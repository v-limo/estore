namespace EStoreAPI.Services.Implementations;

public class ProductService(ApplicationDbContext context, IMapper mapper)
    : CrudService<ProductDto, Product, ProductCreateDto, ProductUpdateDto>(context, mapper), IProductService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;


    public async Task<List<ProductDto>> GetByNameAsync(string name)
    {
        try
        {
            var products = await _context.Products
                .Where(x => x.Name.Contains(name.Trim()))
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(products) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> GetReviewAverageAsync(int productId)
    {
        try
        {
            var reviews = await _context.Reviews
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            if (reviews.Count == 0) return 0;

            return (int)reviews.Average(x => x.Rating);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}