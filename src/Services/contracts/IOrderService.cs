

namespace Backend.Services.contracts;

public interface IOrderService : ICrudService<Order> {
    Task<List<Order>> GetByCustomerAsync(Guid customerId);
    Task<List<Order>> GetByProductAsync(Guid productId);
}