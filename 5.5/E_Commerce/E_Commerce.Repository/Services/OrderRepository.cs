using E_Commerce.Dal;
using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class OrderRepository : IOrderRepository
{
    private readonly MainContext MainContext;

    public OrderRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> AddOrderAsync(Order order)
    {
        await MainContext.AddAsync(order);
        await MainContext.SaveChangesAsync();
        return order.OrderId;
    }

    public async Task DeleteOrderAsync(long customerId, long orderId)
    {
        var order = await GetOrderByIdAsync(customerId, orderId);
        MainContext.Remove(order);
        await MainContext.SaveChangesAsync();
    }

    public async Task<ICollection<Order>> GetAllOrdersAsync()
    {
        var orders = await MainContext.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderByIdAsync(long customerId, long orderId)
    {
        var orders = await GetAllOrdersAsync();
        var order = orders.FirstOrDefault(o => o.OrderId == orderId && o.CustomerId == customerId);
        return order;
    }
}
