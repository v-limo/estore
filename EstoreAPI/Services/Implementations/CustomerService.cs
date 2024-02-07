namespace EStoreAPI.Services.Implementations;

public class CustomerService :
    CrudService<CustomerDto, Customer, CustomerCreateDto, CustomerUpdateDto>, ICustomerService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> GetByNameAsync(string name)
    {
        try
        {
            var customers = await _context.Customers
                .Where(x => x.Name.Contains(name.Trim().ToLower())).ToListAsync();

            return _mapper.Map<List<CustomerDto>>(customers) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
