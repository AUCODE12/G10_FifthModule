using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface ICustomerRepository
{
    Task<long> InsertCustomerAsync(Customer customer);
    Task<List<Customer>> SelectAllCustomersAsync();
    Task<Customer> SelectCustomerByIdAsync(long customerId);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerByIdAsync(long customerId);
}