namespace EStoreAPI.Services.Contracts;

public interface ICategoryService : ICrudService<CategoryDto, CategoryCreateDto, CategoryUpdateDto>
{
    Task<List<CategoryDto>> GetCategoriesByName(string name);
}
