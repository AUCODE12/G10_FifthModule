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

    public async Task<long> AddCustomerAsync(Customer customer)
    {
        await MainContext.Customers.AddAsync(customer);
        await MainContext.SaveChangesAsync();
        return customer.CustomerId;
    }

    public async Task DeleteCustomerAsync(long customerId)
    {
        var customer = await GetCustomerByCustomerIdAsync(customerId);
        MainContext.Customers.Remove(customer);
        await MainContext.SaveChangesAsync();
    }

    public async Task<ICollection<Customer>> GetAllCustomers()
    {
        return await MainContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerByCustomerIdAsync(long customerId)
    {
        var customer = await MainContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        return customer;
    }

    public async Task<Customer> GetCustomerByEmailAsync(string email)
    {
        var customers = await GetAllCustomers();
        return customers.FirstOrDefault(c => c.Email == email);
    }

    public Task UpdateCustomerAsync(Customer updatedCustomer)
    {
        MainContext.Customers.Update(updatedCustomer);
        return MainContext.SaveChangesAsync();
    }
}
