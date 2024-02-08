namespace EStoreAPI.Services.Contracts;

public interface IOrderService : ICrudService<OrderDto, OrderCreateDto, OrderUpdateDto>
{
    Task<List<OrderDto>> GetByCustomerAsync(int customerId);
    Task<List<OrderDto>?> GetByProductAsync(int productId);
}