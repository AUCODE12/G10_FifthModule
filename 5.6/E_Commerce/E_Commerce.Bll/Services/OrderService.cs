
using E_Commerce.Bll.Dtos.CartDto;
using E_Commerce.Bll.Dtos.CartProductDto;
using E_Commerce.Bll.Dtos.OrderDto;
using E_Commerce.Bll.Dtos.OrderProductDto;
using E_Commerce.Bll.Dtos.ProductDto;
using E_Commerce.Dal.Entities;
using E_Commerce.PaymentBroker.Models;
using E_Commerce.PaymentBroker.Services;
using E_Commerce.Repository.Services;

namespace E_Commerce.Bll.Services;

public class OrderService : IOrderService
{
    private readonly ICartRepository CartRepository;
    private readonly ICustomerRepository CustomerRepository;
    private readonly ICardRepository CardRepository;
    private readonly IPaymentService PaymentService;
    private readonly IOrderRepository OrderRepository;
    private readonly IOrderProductRepository OrderProductRepository;
    private readonly IProductRepository ProductRepository;

    public OrderService(ICartRepository cartRepository, ICustomerRepository customerRepository, ICardRepository cardRepository, IPaymentService paymentService, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IProductRepository productRepository)
    {
        CartRepository = cartRepository;
        CustomerRepository = customerRepository;
        CardRepository = cardRepository;
        PaymentService = paymentService;
        OrderRepository = orderRepository;
        OrderProductRepository = orderProductRepository;
        ProductRepository = productRepository;
    }

    public async Task MakePaymentAsync(long customerId)
    {
        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        if (customer is null) throw new Exception("Customer not found in MakePaymentAsync");

        var selectedCard = await CardRepository.SelectSelectedCardByCustomerIdAsync(customerId);
        if (selectedCard is null) throw new Exception("Card is not exist in MakePaymentAsync");

        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId, true);
        if (cart is null) throw new Exception("Cart is empty can not be CheckOuted");

        var paymeTransactionRequestDto = new IPaymeTransactionRequestDto()
        {
            TotalPrice = cart.CartProducts.Sum(c => c.Quantity * c.Product.Price),
            CustomerPhoneNumber = customer.PhoneNumber,
            Card = selectedCard,
        };

        await PaymentService.ProcessPaymeTansaction(paymeTransactionRequestDto);

        var order = new Order()
        {
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = cart.CartProducts.Sum(c => c.Quantity * c.Product.Price),
            Discount = 0,
            DiscountPercentage = 0,
            ServicePrice = 0,
            StatusId = 6
        };

        var orderId = await OrderRepository.InsertOrderAsync(order);

        var orderProducts = cart.CartProducts.Select(c => new OrderProduct()
        {
            OrderId = orderId,
            ProductId = c.ProductId,
            Quantity = c.Quantity
        }).ToList();

        await OrderProductRepository.InsertOrderProductsAsync(orderProducts);

        foreach (var cartProduct in cart.CartProducts)
        {
            var product = cartProduct.Product;
            product.StockQuantity -= cartProduct.Quantity;
            await ProductRepository.UpdateProductAsync(product);
        }

        await CartRepository.ClearCartAsync(customerId);
    }

    public async Task<GetOrderDto> CheckOutOrderAsync(long customerId)
    {
        var customer = await CustomerRepository.SelectCustomerByIdAsync(customerId);
        if (customer is null) throw new Exception("Customer not found in CheckOutOrderAsync");

        var cart = await CartRepository.GetCartByCustomerIdAsync(customerId, true);
        if (cart is null) throw new Exception("Cart is empty can not be CheckOuted");

        var getOrderDto = new GetOrderDto()
        {
            CustomerId = cart.CustomerId,
            CreatedAt = cart.CreatedAt,
            TotalAmount = cart.CartProducts.Sum(c => c.Quantity * c.Product.Price),
            Discount = 0,
            DiscountPercentage = 0,
            ServicePrice = 0,
            OrderStatus = "Pending",
            GetOrderProductDtos = cart.CartProducts.Select(cp => ConvertCartProductToOrderProductDto(cp)).ToList()
        };

        return getOrderDto;
    }

    private GetOrderProductDto ConvertCartProductToOrderProductDto(CartProduct cartProduct)
    {
        var getOrderProductDto = new GetOrderProductDto()
        {
            Quantity = cartProduct.Quantity,
            ProductId = cartProduct.ProductId,
            GetProductDto = ConvertProductToDto(cartProduct.Product)
        };

        return getOrderProductDto;
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
}
