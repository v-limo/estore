namespace EStoreAPI.Controllers;

public class CustomerController : CrudController<CustomerDto, CustomerCreateDto, CustomerUpdateDto>
{
    private readonly ICustomerService _customerService;


    public CustomerController(ICustomerService customerService) : base(customerService)
    {
        _customerService = customerService;
    }


    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CustomerDto>>?> SearchCustomers(string name)
    {
        return await _customerService.GetByNameAsync(name);
    }
}
