using E_Commerce.Repository.Services;

namespace E_Commerce.Bll.Services;

public class CartService : ICartService
{
    private readonly ICartRepository CartRepository;

    public CartService(ICartRepository cartRepository)
    {
        CartRepository = cartRepository;
    }

    public async Task AddProductToCardAsync(long customerId, long productId, int quantity)
    {
        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart == null)
        {
            cart = await CartRepository.CreateCartAsync(customerId);
        }

        //var cartProduct = new List<CartProduct>();
    }
}
