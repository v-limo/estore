namespace Backend.Controllers;

public class CategoryController : CrudController<CategoryDto, CategoryCreateDto, CategoryUpdateDto>
{
    private readonly ICategoryService _categoryService;

    public CategoryController(
        ICategoryService categoryService) : base(categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CategoryDto>>?> SearchCategories(string name)
    {
        return await _categoryService.GetCategoriesByName(name);
    }
}