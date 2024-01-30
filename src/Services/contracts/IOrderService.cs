namespace Backend.Services.contracts;

public interface IOrderService : ICrudService<Order>
{
    Task<List<Order>> GetByCustomerAsync(int customerId);
    Task<List<Order>> GetByProductAsync(int productId);
}