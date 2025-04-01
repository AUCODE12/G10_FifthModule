using E_Commerce.Dal;
using E_Commerce.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CartRepository : ICartRepository
{
    private readonly MainContext MainContext;

    public CartRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task ClearCartAsync(long customerId)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        if (cart == null)
        {
            throw new Exception("Cart not found ClearCartAsync");
        }
        cart.CartProducts.Clear();
        await MainContext.SaveChangesAsync();
    }

    public async Task<Cart> CreateCartAsync(long cusotmerId)
    {
        var cart = new Cart
        {
            CustomerId = cusotmerId,
            CreatedAt = DateTime.UtcNow,
        };
        await MainContext.Carts.AddAsync(cart);
        await MainContext.SaveChangesAsync();
        return cart;
    }

    public async Task DeleteCartAsync(long customerId)
    {
        var cart = await GetCartByCustomerIdAsync(customerId);
        MainContext.Carts.Remove(cart);
        await MainContext.SaveChangesAsync();
    }

    public async Task<Cart?> GetCartByCustomerIdAsync(long customerId)
    {
        var cart = await MainContext.Carts.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        return cart;
    }

    public Task UpdateCartAsync(Cart cart)
    {
        MainContext.Carts.Update(cart);
        return MainContext.SaveChangesAsync();
    }
}
