namespace EStoreAPI.Services.Implementations;

public class UnitOfWork(
    DbContext context,
    ICategoryService categoryService,
    IProductService productService,
    IOrderService orderService,
    ICustomerService customerService,
    IReviewService reviewService)
    : IUnitOfWork, IDisposable
{
    public void Dispose()
    {
        context.Dispose();
    }


    public ICategoryService CategoryService { get; } = categoryService;
    public IProductService ProductService { get; } = productService;
    public IOrderService OrderService { get; } = orderService;
    public ICustomerService CustomerService { get; } = customerService;
    public IReviewService ReviewService { get; } = reviewService;

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}