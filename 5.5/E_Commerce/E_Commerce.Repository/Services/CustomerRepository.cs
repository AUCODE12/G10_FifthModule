using E_Commerce.Dal;
using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly MainContext MainContext;
    
    public CustomerRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public Task<long> AddCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCustomerAsync(long customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetCustomerByCustomerIdAsync(long customerId)
    {
        var customer = await MainContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        return customer;
    }

    public Task<Customer> GetCustomerByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomerAsync(Customer updatedCustomer)
    {
        throw new NotImplementedException();
    }
}
