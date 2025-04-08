using E_Commerce.Bll.Dtos.CustomerDto;
using E_Commerce.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers;

[Route("api/auth")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService CustomerService;
    
    public CustomerController(ICustomerService customerService)
    {
        CustomerService = customerService;
    }

    [HttpPost("register")]
    public async Task<long> RegisterCustomer([FromBody] GetCustomerDto getCustomerDto)
    {
        return await CustomerService.RegisterCustomerAsync(getCustomerDto);
    }

    [HttpGet("myProfile")]
    public async Task<GetCustomerDto> GetProfile(long customerId)
    {
        return await CustomerService.GetProfileByCustomerIdAsync(customerId);
    }

    [HttpGet("all")]
    public async Task<List<GetCustomerDto>> GetAllCustomers()
    {
        return await CustomerService.GetAllCustomersAsync();
    }

    [HttpPut("profile")]
    public async Task PutProfile([FromBody] GetCustomerDto getCustomerDto)
    {
        await CustomerService.PutProfileCustomerAsync(getCustomerDto);
    }

    [HttpDelete("delete")]
    public async Task DeleteCustomer(long customerId)
    {
        await CustomerService.DeleteCustomerByIdAsync(customerId);
    }
}
