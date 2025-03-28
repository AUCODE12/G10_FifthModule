
using E_Commerce.Bll.Dtos.CartDto;
using E_Commerce.Dal.Entites;
using E_Commerce.Repository.Services;

namespace E_Commerce.Bll.Services;

public class CartService : ICartService
{
    private readonly ICartRepository CartRepository;
    private readonly ICartProductRepository CartProductRepository;
    private readonly ICustomerRepository CustomerRepository;
    private readonly IProductRepository ProductRepository;


    public CartService(ICartRepository cartRepository, ICustomerRepository customerRepository, ICartProductRepository cartProductRepository, IProductRepository productRepository)
    {
        CartRepository = cartRepository;
        CustomerRepository = customerRepository;
        CartProductRepository = cartProductRepository;
        ProductRepository = productRepository;
    }

    public async Task AddProductToCartAsync(long customerId, long productId, int quantity)
    {
        var product = await ProductRepository.SelectProductByIdAsync(productId);
        if(product == null || product.StockQuantity < quantity)
        {
            throw new Exception("Not enough quantity or not found");
        }

        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        if (customer == null) throw new Exception("Customer not found in AddProductToCartAsync");
        
        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart == null)
        {
            cart = await CartRepository.CreateCartAsync(customerId);
        }
        

        var cartProducts = await CartProductRepository.SelectCartProductsByCartIdAsync(cart.CartId);

        var productExists = cartProducts.Any(x => x.ProductId == productId);

        if(productExists == true)
        {
            var cartProduct = cartProducts.FirstOrDefault(x => x.ProductId == productId);

            if(quantity <= 0)
            {
                await CartProductRepository.DeleteCartProductByIdAsync(cartProduct.CartProductId);
            }
            else
            {
                cartProduct.Quantity = quantity;
                await CartProductRepository.UpdateCartProductAsync(cartProduct);
            }
        }
        else if(quantity > 0)
        {
            var newCartProduct = new CartProduct()
            {
                Quantity = quantity,
                ProductId = productId,
                CartId = cart.CartId,
            };

            await CartProductRepository.InsertCartProductAsync(newCartProduct);
        }
    }

    public Task<GetCartDto> GetCartByCustomerId(long customerId)
    {
        throw new NotImplementedException();
    }
}
