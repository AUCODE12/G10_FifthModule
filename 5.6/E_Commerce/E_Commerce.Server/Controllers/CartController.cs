using E_Commerce.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService CartService;

    public CartController(ICartService cartService)
    {
        CartService = cartService;
    }

    [HttpPost("add")]
    public async Task AddProduct(long customerId, long productId, int quantity)
    {
        await CartService.AddProductToCardAsync(customerId, productId, quantity);
    }
}
