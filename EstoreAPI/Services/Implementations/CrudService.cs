namespace Backend.Services.Implementations;

public class CrudService<TDto, TEntity, TCreateDto, TUpdateDto> : ICrudService<TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseClass
    where TCreateDto : class
    where TUpdateDto : class
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    protected CrudService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<List<TDto>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            return entity is not null ? _mapper.Map<TDto>(entity) : default;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<TDto?> CreateAsync(TCreateDto model)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(model);
            if (entity == null) return default;
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null) return default;

            _mapper.Map(updateDto, entity);

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return false;
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}