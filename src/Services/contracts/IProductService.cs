using Backend.Models;

namespace Backend.Services.contracts;

public interface IProductService : ICrudService<Product> {
  Task<List<Product>> GetByNameAsync(string name);
}