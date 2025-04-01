using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface ICartProductRepository
{
    Task<long> InsertCartProductAsync(CartProduct cartProduct);
    Task DeleteCartProductAsync(long cartProductId);
    Task<List<CartProduct>> SelectCartProductsByCartIdAsync(long cartId);
    Task UpdateCartProductAsync(CartProduct cartProduct);
}
