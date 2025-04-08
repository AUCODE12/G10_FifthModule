using E_Commerce.Bll.Dtos.CustomerDto;
using E_Commerce.Dal.Entities;
using E_Commerce.Repository.Services;

namespace E_Commerce.Bll.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository CustomerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        CustomerRepository = customerRepository;
    }

    public async Task DeleteCustomerByIdAsync(long customerId)
    {
        await CustomerRepository.DeleteCustomerByIdAsync(customerId);
    }

    public async Task<List<GetCustomerDto>> GetAllCustomersAsync()
    {
        var customers = await CustomerRepository.SelectAllCustomersAsync();
        return customers.Select(c => ConvertCustomerToDto(c)).ToList();
    }

    public async Task<GetCustomerDto> GetProfileByCustomerIdAsync(long customerId)
    {
        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        return ConvertCustomerToDto(customer);
    }

    public async Task PutProfileCustomerAsync(GetCustomerDto getCustomerDto)
    {
        var customer = ConvertDtoToCustomer(getCustomerDto);
        await CustomerRepository.UpdateCustomerAsync(customer);
    }

    public async Task<long> RegisterCustomerAsync(GetCustomerDto getCustomerDto)
    {
        var customers = await GetAllCustomersAsync();
        var existingCustomer = customers.FirstOrDefault(c => c.PhoneNumber == getCustomerDto.PhoneNumber || c.Email == getCustomerDto.Email);

        if (existingCustomer is not null) throw new Exception("Customer already exists with this phone number or email");
        var customer = ConvertDtoToCustomer(getCustomerDto);
        var customerId = await CustomerRepository.InsertCustomerAsync(customer);
        return customerId;
    }

    private GetCustomerDto ConvertCustomerToDto(Customer customer)
    {
        return new GetCustomerDto()
        {
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber
        };
    }

    private Customer ConvertDtoToCustomer(GetCustomerDto getCustomerDto)
    {
        return new Customer()
        {
            Email = getCustomerDto.Email,
            FirstName = getCustomerDto.FirstName,
            LastName = getCustomerDto.LastName,
            PhoneNumber = getCustomerDto.PhoneNumber
        };
    }
}
