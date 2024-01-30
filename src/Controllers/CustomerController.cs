namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Customer>?> CreateCustomer(Customer? customer)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (customer == null) return null;
        var createdCustomer = await customerService.CreateAsync(customer);

        if (createdCustomer != null)
            return CreatedAtAction(nameof(GetCustomer), new { createdCustomer.Id }, customer);

        return null;
    }


    [HttpGet]
    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await customerService.GetAllAsync();
    }


    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<Customer?>> GetCustomer(int customerId)
    {
        var customer = await customerService.GetByIdAsync(customerId);
        return customer;
    }


    [HttpPut("{customerId:int}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(int customerId, Customer customer)
    {
        if (customerId != customer.Id || !ModelState.IsValid)
            return BadRequest(
                new
                {
                    Message = "Customer Id mismatch or invalid data",
                    CustomerId = customerId,
                    status = StatusCodes.Status400BadRequest
                }
            );

        var updatedCustomer = await customerService.UpdateAsync(customerId, customer);
        if (updatedCustomer is null)
            return NotFound(
                new { Message = "Customer not found so cannot update", CustomerId = customerId }
            );
        return updatedCustomer;
    }


    [HttpDelete("{customerId:int}")]
    public async Task<ActionResult<bool>> DeleteCustomer(int customerId)
    {
        var result = await customerService.DeleteAsync(customerId);
        if (!result)
            return NotFound(
                new { Message = "Customer not found so cannot delete", CustomerId = customerId }
            );
        // return result; ////alternativly
        return NoContent();
    }
}