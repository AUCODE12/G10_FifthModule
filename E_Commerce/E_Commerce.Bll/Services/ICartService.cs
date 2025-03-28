using E_Commerce.Bll.Dtos.CartDto;

namespace E_Commerce.Bll.Services;

public interface ICartService
{
    Task AddProductToCartAsync(long customerId, long ProductId, int quantity);
    Task<GetCartDto> GetCartByCustomerId(long customerId);
}