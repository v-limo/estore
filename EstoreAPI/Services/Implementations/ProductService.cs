namespace EStoreAPI.Services.Implementations;

public class ProductService : CrudService<ProductDto, Product, ProductCreateDto, ProductUpdateDto>, IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }


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
