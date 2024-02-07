namespace Backend.Services.Contracts;

public interface ICustomerService : ICrudService<CustomerDto, CustomerCreateDto, CustomerUpdateDto>
{
    Task<List<CustomerDto>> GetByNameAsync(string name);
}