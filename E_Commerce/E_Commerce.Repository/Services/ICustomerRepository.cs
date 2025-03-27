using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface ICustomerRepository
{
    Task<long> InsertCustomerAsync(Customer customer);
    Task<Customer> SelectCustomerByIdAsync(long customerId);
    Task DeleteCustomerByIdAsync(long customerId);
}