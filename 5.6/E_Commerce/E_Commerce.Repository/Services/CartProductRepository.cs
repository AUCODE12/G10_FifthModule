using E_Commerce.Dal;
using E_Commerce.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CartProductRepository : ICartProductRepository
{
    private readonly MainContext MainContext;

    public CartProductRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task DeleteCartProductAsync(long cartProductId)
    {
        var cartProduct = await MainContext.CartProducts.FirstOrDefaultAsync(x => x.CartProductId == cartProductId);
        if (cartProduct is null)
        {
            throw new Exception("CartProduct not found in DeleteCartProductAsync");
        }
        MainContext.CartProducts.Remove(cartProduct);
        await MainContext.SaveChangesAsync();
    }

    public async Task<long> InsertCartProductAsync(CartProduct cartProduct)
    {
        await MainContext.CartProducts.AddAsync(cartProduct);
        await MainContext.SaveChangesAsync();
        return cartProduct.CartProductId;
    }

    public async Task<List<CartProduct>> SelectCartProductsByCartIdAsync(long cartId)
    {
        var cartProducts = await MainContext.CartProducts.Where(x => x.CartId == cartId).ToListAsync();
        return cartProducts;
    }

    public async Task UpdateCartProductAsync(CartProduct cartProduct)
    {
        MainContext.CartProducts.Update(cartProduct);
        await MainContext.SaveChangesAsync();
    }
}
