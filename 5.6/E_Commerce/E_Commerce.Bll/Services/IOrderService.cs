using E_Commerce.Bll.Dtos.OrderDto;

namespace E_Commerce.Bll.Services;

public interface IOrderService
{
    Task<GetOrderDto> CheckOutOrderAsync(long customerId);
    Task MakePaymentAsync(long customerId);
}
