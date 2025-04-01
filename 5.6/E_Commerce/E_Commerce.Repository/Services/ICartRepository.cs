using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface ICartRepository
{
    Task<Cart> CreateCartAsync(long cusotmerId);
    Task ClearCartAsync(long customerId);
    Task<Cart> GetCartByCustomerIdAsync(long customerId);
    Task DeleteCartAsync(long customerId);
    Task UpdateCartAsync(Cart cart);
}