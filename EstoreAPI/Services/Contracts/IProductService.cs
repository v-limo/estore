namespace EStoreAPI.Services.Contracts;

public interface IProductService : ICrudService<ProductDto, ProductCreateDto, ProductUpdateDto>
{
    Task<List<ProductDto>> GetByNameAsync(string name);
    Task<int> GetReviewAverageAsync(int productId);
}