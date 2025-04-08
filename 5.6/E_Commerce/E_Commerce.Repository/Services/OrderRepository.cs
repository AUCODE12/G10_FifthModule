using E_Commerce.Dal;
using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public class OrderRepository : IOrderRepository
{
    private readonly MainContext MainContext;

    public OrderRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> InsertOrderAsync(Order order)
    {
        await MainContext.Orders.AddAsync(order);
        await MainContext.SaveChangesAsync();
        return order.OrderId;
    }

    public Task<Order> SelectOrderByOrderIdAsync(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> SelectOrdersByCustomerIdAsync(long CustomerId)
    {
        throw new NotImplementedException();
    }
}
