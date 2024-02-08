namespace EStoreAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "Admin")]
public abstract class CrudController<TDto, TCreate, TUpdate> : ApiControllerBase
    where TDto : class, IIdentifiable
    where TCreate : class
    where TUpdate : class, IIdentifiable
{
    private readonly ICrudService<TDto, TCreate, TUpdate> _crudService;

    protected CrudController(ICrudService<TDto, TCreate, TUpdate> crudService)
    {
        _crudService = crudService;
    }


    [HttpPost]
    public async Task<ActionResult<TDto>?> Create(TCreate entity)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdEntity = await _crudService.CreateAsync(entity);

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
        return await _crudService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<TDto?>> Get(int id)
    {
        var entity = await _crudService.GetByIdAsync(id);
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

        var updatedEntity = await _crudService.UpdateAsync(id, entity);
        if (updatedEntity is null)
            return NotFound(
                new { Message = "Entity not found so cannot update", Id = id }
            );
        return updatedEntity;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var deleted = await _crudService.DeleteAsync(id);
        return deleted;
    }
}