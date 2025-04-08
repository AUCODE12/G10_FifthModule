using E_Commerce.Bll.Dtos.CustomerDto;

namespace E_Commerce.Bll.Services;

public interface ICustomerService
{
    Task<long> RegisterCustomerAsync(GetCustomerDto getCustomerDto);
    //Task<long> LoginCustomer()
    Task<GetCustomerDto> GetProfileByCustomerIdAsync(long customerId);
    Task<List<GetCustomerDto>> GetAllCustomersAsync();
    Task PutProfileCustomerAsync(GetCustomerDto getCustomerDto);
    Task DeleteCustomerByIdAsync(long customerId);
}