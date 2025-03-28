using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public class OrderRepository : IOrderRepository
{
    public Task<long> InsertOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> SelectOrderByOrderId(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> SelectOrdersByCustomerId(long customerId)
    {
        throw new NotImplementedException();
    }
}
