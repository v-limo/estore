namespace EStoreAPI.Controllers;

public class CustomerController(ICustomerService customerService)
    : CrudController<CustomerDto, CustomerCreateDto, CustomerUpdateDto>(customerService)
{
    [HttpGet]
    [Route("search")]
    [AllowAnonymous]
    public async Task<ActionResult<List<CustomerDto>>?> SearchCustomers(string name)
    {
        return await customerService.GetByNameAsync(name);
    }
}