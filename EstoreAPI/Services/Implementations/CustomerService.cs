namespace EStoreAPI.Services.Implementations;

public class CustomerService(ApplicationDbContext context, IMapper mapper) :
    CrudService<CustomerDto, Customer, CustomerCreateDto, CustomerUpdateDto>(context, mapper), ICustomerService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<CustomerDto>> GetByNameAsync(string name)
    {
        try
        {
            var customers = await _context.Customers
#pragma warning disable CA1862
                .Where(x => x.Name.Contains(name.Trim().ToLower())).ToListAsync();
#pragma warning restore CA1862

            return _mapper.Map<List<CustomerDto>>(customers) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}