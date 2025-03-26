using E_Commerce.Dal.Entites;
using System.Collections;

namespace E_Commerce.Repository.Services;

public interface ICustomerRepository
{
    Task<long> AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerByCustomerIdAsync(long customerId);
    Task<Customer> GetCustomerByEmailAsync(string email);
    Task UpdateCustomerAsync(Customer updatedCustomer);
    Task DeleteCustomerAsync(long customerId);
    Task<ICollection<Customer>> GetAllCustomers();
}