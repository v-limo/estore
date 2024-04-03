namespace EStoreAPI.Services.Contracts;

public interface IUnitOfWork
{
    ICategoryService CategoryService { get; }
    IProductService ProductService { get; }
    IOrderService OrderService { get; }
    ICustomerService CustomerService { get; }
    IReviewService ReviewService { get; }
    Task<bool> SaveChangesAsync();
}