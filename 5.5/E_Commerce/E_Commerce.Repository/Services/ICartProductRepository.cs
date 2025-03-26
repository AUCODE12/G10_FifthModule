using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface ICartProductRepository
{
    Task<long> AddCardProductAsync(CartProduct cartProduct);

    //Task<CartProduct> GetCartProductByCartProductIdAsync(long cartProductId);
    Task<CartProduct> GetCartProductByCartIdAndProductIdAsync(long cartId, long productId);
    Task<ICollection<CartProduct>> GetCartProductByCustomerId(long cartId);
    Task UpdateCartProductAsync(CartProduct updatedCartProduct);
    Task<decimal> GetTotalAmountByCartIdAsync(long cartId);
}