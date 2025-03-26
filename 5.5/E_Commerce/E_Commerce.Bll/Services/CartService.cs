using E_Commerce.Dal.Entites;
using E_Commerce.Repository.Services;
using System.Linq;

namespace E_Commerce.Bll.Services;

public class CartService : ICartService
{
    private readonly ICartRepository CartRepository;
    private readonly IProductRepository ProductRepository;
    private readonly ICartProductRepository CartProductRepository;
    private readonly ICustomerRepository CustomerRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository, ICartProductRepository cartProductRepository, ICustomerRepository customerRepository)
    {
        CartRepository = cartRepository;
        ProductRepository = productRepository;
        CartProductRepository = cartProductRepository;
        CustomerRepository = customerRepository;
    }

    public async Task AddProductToCartAsync(long customerId, long productId, int quantity)
    {
        var customer = await CustomerRepository.GetCustomerByCustomerIdAsync(customerId);
        if (customer is null)
        {
            throw new Exception("Customer not found");
        }
        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart == null)
        {
            cart = await CartRepository.CreateCartAsync(customerId);
        }
        //var cartProducts = new List<CartProduct>();
        var product = await ProductRepository.GetProductByProductIdAsync(productId);
        if (product.StockQuantity < quantity)
        {
            throw new Exception("Not enough stock");
        }
        var cartProduct = await CartProductRepository.GetCartProductByCartIdAndProductIdAsync(cart.CartId, productId);
        if (cartProduct is null)
        {
            cartProduct = new CartProduct()
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity,
            };
            await CartProductRepository.AddCardProductAsync(cartProduct);
        }
        else
        {
            cartProduct.Quantity = quantity;
            await CartProductRepository.UpdateCartProductAsync(cartProduct);
        }
    }

    public async Task ClearCartAsync(long customerId)
    {
        var customer = await CustomerRepository.GetCustomerByCustomerIdAsync(customerId);
        if (customer is null)
        {
            throw new Exception("Customer not found");
        }
        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart is null)
        {
            throw new Exception("Cart not found by CustomerId (ClearCart)");
        }
        await CartRepository.ClearCartAsync(customerId);
    }

    public async Task<decimal> GetTotalAmountByCart(long customerId)
    {
        var customer = await CustomerRepository.GetCustomerByCustomerIdAsync(customerId);
        if (customer is null)
        {
            throw new Exception("Customer not found");
        }
        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId);
        if (cart is null)
        {
            throw new Exception("Cart not found by CustomerId");
        }
        var cartProducts = await CartProductRepository.GetCartProductByCustomerId(cart.CartId);
        var totalSum = cartProducts.Sum(cp => cp.Quantity * cp.Product.Price);
        return totalSum;
        //return await CartProductRepository.GetTotalAmountByCartIdAsync(cart.CartId);
    }
}
