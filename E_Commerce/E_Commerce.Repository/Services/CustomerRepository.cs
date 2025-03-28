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
    public Task DeleteCustomerByIdAsync(long customerId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> SelectCustomerByIdAsync(long customerId)
    {
        var customer = await MainContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
        return customer;
    }
}
