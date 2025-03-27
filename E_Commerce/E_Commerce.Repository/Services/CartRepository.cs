
using E_Commerce.Dal;
using E_Commerce.Dal.Entites;
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
            throw new Exception("Cart not found in ClearCartAsync");
        }

        cart.CartProducts.Clear();
        await MainContext.SaveChangesAsync();
    }

    public async Task<Cart> CreateCartAsync(long customerId)
    {
        var cart = new Cart()
        {
            CustomerId = customerId,
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
        var carts = await MainContext.Carts.Where(c => c.CustomerId == customerId).ToListAsync();
        
        if(carts == null || carts.Count == 0) return null;

        if(carts.Count == 1) return carts[0];

        var cart = carts.Max(c => c.CartId);

        foreach( var cartItem in carts)
        {
            if(cartItem.CartId != cart)
            {
                await DeleteCartAsync(cartItem.CartId);
            }
        }

        return carts[0];
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        MainContext.Carts.Update(cart);
        await MainContext.SaveChangesAsync();
    }
}
