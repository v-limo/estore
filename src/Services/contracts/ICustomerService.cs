namespace Backend.Services.contracts;

public interface ICustomerService : ICrudService<Customer>
{
    Task<List<Customer>> GetByNameAsync(string name);
}