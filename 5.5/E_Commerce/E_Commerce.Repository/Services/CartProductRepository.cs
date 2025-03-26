
using E_Commerce.Dal;
using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class CartProductRepository : ICartProductRepository
{
    private readonly MainContext MainContext;

    public CartProductRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> AddCardProductAsync(CartProduct cartProduct)
    {
        await MainContext.CartProducts.AddAsync(cartProduct);
        await MainContext.SaveChangesAsync();
        return cartProduct.CartProductId;
    }

    public Task<CartProduct> GetCartProductByCartIdAndProductIdAsync(long cartId, long productId)
    {
        var cartProduct = MainContext.CartProducts.FirstOrDefaultAsync(cp => cp.CartId == cartId && cp.ProductId == productId);
        return cartProduct;
    }

    public async Task<ICollection<CartProduct>> GetCartProductByCustomerId(long cartId)
    {
        var cartProduct = MainContext.CartProducts.Where(cp => cp.CartId == cartId).ToList();
        return cartProduct;
    }

    public async Task<decimal> GetTotalAmountByCartIdAsync(long cartId)
    {
        var totalAmount = await MainContext.CartProducts.Where(cp => cp.CartId == cartId).SumAsync(cp => cp.Quantity * cp.Product.Price);
        return totalAmount;
    }

    public async Task UpdateCartProductAsync(CartProduct updatedCartProduct)
    {
        MainContext.CartProducts.Update(updatedCartProduct);
        await MainContext.SaveChangesAsync();
    }

    //public async Task<CartProduct> GetCartProductByCartProductIdAsync(long cartProductId)
    //{
    //    var cartProduct = await MainContext.CartProducts.FirstOrDefaultAsync(cp => cp.CartProductId == cartProductId);
    //    return cartProduct;
    //}
}
