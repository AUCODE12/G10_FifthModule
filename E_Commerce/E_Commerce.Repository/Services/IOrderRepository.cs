using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface IOrderRepository
{
    Task<long> InsertOrderAsync(Order order);
    Task<Order> SelectOrderByOrderId(long orderId);
    Task<List<Order>> SelectOrdersByCustomerId(long customerId);
}