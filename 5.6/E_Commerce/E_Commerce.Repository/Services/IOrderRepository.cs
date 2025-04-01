using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface IOrderRepository
{
    Task<long> InsertOrderAsync(Order order);
    Task<Order> SelectOrderByOrderIdAsync(long orderId);
    Task<List<Order>> SelectOrdersByCustomerIdAsync(long CustomerId);
    //Task<List<Order>> SelectAllOrdersAsync();
    //Task UpdateOrderAsync(Order order);
    //Task DeleteOrderByIdAsync(long orderId);
}