namespace EStoreAPI.Services.Implementations;

public class ReviewService(ApplicationDbContext context, IMapper mapper)
    : CrudService<ReviewDto, Review, ReviewCreateDto, ReviewUpdateDto>(context, mapper), IReviewService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    async Task<List<ReviewDto>> IReviewService.GetReviewsByUserId(int userId)
    {
        try
        {
            var reviews = await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return _mapper.Map<List<ReviewDto>>(reviews) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<List<ReviewDto>> IReviewService.GetReviewsByProductId(int productId)
    {
        try
        {
            var reviews = await _context.Reviews
                .Where(x => x.ProductId == productId)
                .ToListAsync();
            return _mapper.Map<List<ReviewDto>>(reviews) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}