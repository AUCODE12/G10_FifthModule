using E_Commerce.Bll.Dtos.CartDto;
using E_Commerce.Dal.Entities;

namespace E_Commerce.Bll.Services;

public interface ICartService
{
    Task AddProductToCardAsync(long customerId, long productId, int quantity);
    Task<GetCartDto> GetCartByCustomerIdAsync(long customerId);
}