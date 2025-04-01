using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public class OrderRepository : IOrderRepository
{
    public Task<long> InsertOrderAsync(Order order)
    {
        throw new NotImplementedException();
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
