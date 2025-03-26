using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface IOrderRepository
{
    Task<long> AddOrderAsync(Order order);
    Task<ICollection<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(long customerId, long orderId);
    Task DeleteOrderAsync(long customerId, long orderId);
}