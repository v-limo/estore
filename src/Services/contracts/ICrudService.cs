using Backend.Models;

namespace Backend.Services.contracts;

public interface ICrudService<T>
    where T : BaseModel
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> CreateAsync(T model);
    Task<T?> UpdateAsync(Guid id, T model);
    Task<bool> DeleteAsync(Guid id);
}