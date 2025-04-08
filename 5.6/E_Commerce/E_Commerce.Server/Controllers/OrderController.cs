using E_Commerce.Bll.Dtos.OrderDto;
using E_Commerce.Bll.Dtos.OrderProductDto;
using E_Commerce.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService OrderService;

    public OrderController(IOrderService orderService)
    {
        OrderService = orderService;
    }

    [HttpGet("checkOut")]
    public async Task<GetOrderDto> CheckOutOrder(long customerId)
    {
        return await OrderService.CheckOutOrderAsync(customerId);
    }

    [HttpPost("makePayment")]
    public async Task MakePayment(long customerId)
    {
        await OrderService.MakePaymentAsync(customerId);
    }
}
