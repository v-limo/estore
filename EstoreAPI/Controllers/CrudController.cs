namespace EStoreAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
[Authorize(Roles = "Admin")]
public abstract class CrudController<TDto, TCreate, TUpdate>(ICrudService<TDto, TCreate, TUpdate> crudService)
    : ApiControllerBase
    where TDto : class, IIdentifiable
    where TCreate : class
    where TUpdate : class, IIdentifiable
{
    [HttpPost]
    public async Task<ActionResult<TDto>?> Create(TCreate entity)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdEntity = await crudService.CreateAsync(entity);

        if (createdEntity == null)
            return Ok();
        return CreatedAtAction(nameof(Get), new
        {
            createdEntity.Id
        }, entity);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<TDto?>> GetAll()
    {
        return await crudService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<TDto?>> Get(int id)
    {
        var entity = await crudService.GetByIdAsync(id);
        return entity;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TDto>> Update(int id, TUpdate entity)
    {
        if (!ModelState.IsValid || entity.Id != id)
            return BadRequest(
                new
                {
                    Message = "Id mismatch or invalid data",
                    Id = id,
                    status = StatusCodes.Status400BadRequest
                }
            );

        var updatedEntity = await crudService.UpdateAsync(id, entity);
        if (updatedEntity is null)
            return NotFound(
                new { Message = "Entity not found so cannot update", Id = id }
            );
        return updatedEntity;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var deleted = await crudService.DeleteAsync(id);
        return deleted;
    }
}