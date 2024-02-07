namespace Backend.Services.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, ICategoryService categoryService, IProductService productService,
        IOrderService orderService, ICustomerService customerService, IReviewService reviewService)
    {
        _context = context;
        CategoryService = categoryService;
        ProductService = productService;
        OrderService = orderService;
        CustomerService = customerService;
        ReviewService = reviewService;
    }


    public void Dispose()
    {
        _context.Dispose();
    }


    public ICategoryService CategoryService { get; }
    public IProductService ProductService { get; }
    public IOrderService OrderService { get; }
    public ICustomerService CustomerService { get; }
    public IReviewService ReviewService { get; }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}