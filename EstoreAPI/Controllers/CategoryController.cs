namespace EStoreAPI.Controllers;

public class CategoryController(ICategoryService categoryService)
    : CrudController<CategoryDto, CategoryCreateDto, CategoryUpdateDto>(categoryService)
{
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CategoryDto>>?> SearchCategories(string name)
    {
        return await categoryService.GetCategoriesByName(name);
    }
}