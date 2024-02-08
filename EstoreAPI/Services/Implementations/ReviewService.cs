namespace EStoreAPI.Services.Implementations;

public class ReviewService : CrudService<ReviewDto, Review, ReviewCreateDto, ReviewUpdateDto>, IReviewService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

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
