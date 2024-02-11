namespace EStoreAPI.Controllers;

public class ReviewController : CrudController<ReviewDto, ReviewCreateDto, ReviewUpdateDto>
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService) : base(reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("product/{productId:int}")]
    [AllowAnonymous]
    public async Task<IEnumerable<ReviewDto?>> GetReviewsByProductId(int productId)
    {
        return await _reviewService.GetReviewsByProductId(productId);
    }

    [HttpGet("user/{userId:int}")]
    [AllowAnonymous]
    public async Task<IEnumerable<ReviewDto?>> GetReviewsByUserId(int userId)
    {
        return await _reviewService.GetReviewsByUserId(userId);
    }
}