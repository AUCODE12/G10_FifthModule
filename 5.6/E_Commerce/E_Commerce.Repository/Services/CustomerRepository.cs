using E_Commerce.Dal;
using E_Commerce.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly MainContext MainContext;

    public CustomerRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task DeleteCustomerByIdAsync(long customerId)
    {
        var customer = await SelectCustomerByIdAsync(customerId);
        MainContext.Customers.Remove(customer);
        await MainContext.SaveChangesAsync();
    }

    public async Task<long> InsertCustomerAsync(Customer customer)
    {
        await MainContext.Customers.AddAsync(customer);
        await MainContext.SaveChangesAsync();
        return await Task.FromResult(customer.CustomerId);
    }

    public async Task<List<Customer>> SelectAllCustomersAsync()
    {
        var customers = await MainContext.Customers.ToListAsync();
        return customers;
    }

    public async Task<Customer?> SelectCustomerByIdAsync(long customerId)
    {
        var customer = await MainContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        MainContext.Customers.Update(customer);
        await MainContext.SaveChangesAsync();
    }
}
