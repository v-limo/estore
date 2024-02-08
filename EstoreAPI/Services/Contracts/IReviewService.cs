namespace EStoreAPI.Services.Contracts;

public interface IReviewService : ICrudService<ReviewDto, ReviewCreateDto, ReviewUpdateDto>
{
    Task<List<ReviewDto>> GetReviewsByProductId(int productId);
    Task<List<ReviewDto>> GetReviewsByUserId(int userId);
}
