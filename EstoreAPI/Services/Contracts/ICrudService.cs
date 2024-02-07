namespace Backend.Services.Contracts;

public interface ICrudService<TDto, TCreateDto, in TUpdateDto> where TCreateDto : class
    where TUpdateDto : class
{
    Task<List<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto?> CreateAsync(TCreateDto model);
    Task<TDto?> UpdateAsync(int id, TUpdateDto model);
    Task<bool> DeleteAsync(int id);
}