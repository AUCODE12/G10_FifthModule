using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface ICartProductRepository
{
    Task<long> InsertCartProductAsync(CartProduct cartProduct);
    Task DeleteCartProductByIdAsync(long cartProductId);
    Task<List<CartProduct>> SelectCartProductsByCartIdAsync(long cartId);
    Task UpdateCartProductAsync(CartProduct cartProduct);
}