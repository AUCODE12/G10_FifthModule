using E_Commerce.Bll.Dtos.CartDto;
using E_Commerce.Bll.Dtos.CartProductDto;
using E_Commerce.Bll.Dtos.ProductDto;
using E_Commerce.Dal.Entities;
using E_Commerce.Repository.Services;
using System.Threading.Tasks;

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

    public async Task<GetCartDto> GetCartByCustomerIdAsync(long customerId)
    {
        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        if (customer is null) throw new Exception("Customer not found found in GetCartByCustomerIdAsync");

        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId, true);
        if (cart is null) throw new Exception("Cart is empty in GetCartByCustomerIdAsync");
        
        var getCartDto = new GetCartDto()
        {
            CartId = cart.CartId,
            CustomerId = cart.CustomerId,
            CreatedAt = cart.CreatedAt,
            TotalPrice = cart.CartProducts.Sum(c => c.Quantity * c.Product.Price),
            GetCartProductDtos = cart.CartProducts.Select(c => ConvertCartProductToDto(c)).ToList()
        };

        return getCartDto;
    }

    private GetCartProductDto ConvertCartProductToDto(CartProduct cartProduct)
    {
        var getCartProductDto = new GetCartProductDto()
        {
            CartProductId = cartProduct.CartProductId,
            Quantity = cartProduct.Quantity,
            CartId = cartProduct.CartId,
            ProductId = cartProduct.ProductId,
            GetProductDto = ConvertProductToDto(cartProduct.Product)
        };

        return getCartProductDto;
    }

    private GetProductDto ConvertProductToDto(Product product)
    {
        var getProductDto = new GetProductDto()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageLink = product.ImageLink
        };

        return getProductDto;
    }

    //private async Task<decimal> GetTotalPrice(List<CartProduct> cartProducts)
    //{
    //    //decimal totalPrice = 0;
    //    //foreach (var cartProduct in cartProducts)
    //    //{
    //    //    var product = await ProductRepository.SelectProductByIdAsync(cartProduct.ProductId);
    //    //    if (product != null)
    //    //    {
    //    //        totalPrice += product.Price * cartProduct.Quantity;
    //    //    }
    //    //}
    //    //return totalPrice;
    //}
}
