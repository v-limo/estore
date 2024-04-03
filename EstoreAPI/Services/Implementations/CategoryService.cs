namespace EStoreAPI.Services.Implementations;

public class CategoryService(ApplicationDbContext context, IMapper mapper)
    : CrudService<CategoryDto, Category, CategoryCreateDto, CategoryUpdateDto>(context, mapper),
        ICategoryService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;


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