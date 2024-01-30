namespace Backend.Services.contracts;

public interface ICrudService<T>
    where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> CreateAsync(T model);
    Task<T?> UpdateAsync(int id, T model);
    Task<bool> DeleteAsync(int id);
}