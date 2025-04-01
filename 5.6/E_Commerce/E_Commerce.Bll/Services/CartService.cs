using E_Commerce.Bll.Dtos.CartDto;
using E_Commerce.Dal.Entities;
using E_Commerce.Repository.Services;

namespace E_Commerce.Bll.Services;

public class CartService : ICartService
{
    private readonly ICartRepository CartRepository;
    private readonly ICustomerRepository CustomerRepository;
    private readonly ICartProductRepository CartProductRepository;
    private readonly IProductRepository ProductRepository;

    public CartService(ICartRepository cartRepository, ICustomerRepository customerRepository, ICartProductRepository cartProductRepository, IProductRepository productRepository)
    {
        CartRepository = cartRepository;
        CustomerRepository = customerRepository;
        CartProductRepository = cartProductRepository;
        ProductRepository = productRepository;
    }

    public async Task AddProductToCardAsync(long customerId, long productId, int quantity)
    {
        var product = await ProductRepository.SelectProductByIdAsync(productId);
        if (product is null || product.StockQuantity < quantity) throw new Exception("Not enough stock or not found product in AddProductToCardAsync");

        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        if (customer is null) throw new Exception("Customer not found or not found in AddProductToCardAsync");

        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart is null)
        {
            cart = await CartRepository.CreateCartAsync(customerId);
        }

        var cartProducts = await CartProductRepository.SelectCartProductsByCartIdAsync(cart.CartId);
        var productExists = cartProducts.Any(x => x.ProductId == productId);
        if (productExists is true)
        {
            var cartProduct = cartProducts.FirstOrDefault(x => x.ProductId == productId);
            if (quantity == 0)
            {
                await CartProductRepository.DeleteCartProductAsync(cartProduct.CartProductId);
            }
            else
            {
                cartProduct.Quantity = quantity;
                await CartProductRepository.UpdateCartProductAsync(cartProduct);
            }
        }
        else if (quantity > 0)
        {
            var newCartProduct = new CartProduct()
            {
                Quantity = quantity,
                ProductId = productId,
                CartId = cart.CartId    
            };

            await CartProductRepository.InsertCartProductAsync(newCartProduct);
        }
    }

    public Task<GetCartDto> GetCartByCustomerIdAsync(long customerId)
    {
        throw new NotImplementedException();
    }
}
