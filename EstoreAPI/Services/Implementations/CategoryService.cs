namespace EStoreAPI.Services.Implementations;

public class CategoryService : CrudService<CategoryDto, Category, CategoryCreateDto, CategoryUpdateDto>,
    ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public CategoryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetCategoriesByName(string name)
    {
        try
        {
            var entities = await _context.Categories
                .Where(x => x.Name.Contains(name.Trim())).ToListAsync();
            return _mapper.Map<List<CategoryDto>>(entities) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}