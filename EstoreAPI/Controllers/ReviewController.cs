namespace EStoreAPI.Controllers;

public class ReviewController(IReviewService reviewService)
    : CrudController<ReviewDto, ReviewCreateDto, ReviewUpdateDto>(reviewService)
{
    [HttpGet("product/{productId:int}")]
    [AllowAnonymous]
    public async Task<IEnumerable<ReviewDto?>> GetReviewsByProductId(int productId)
    {
        return await reviewService.GetReviewsByProductId(productId);
    }
}