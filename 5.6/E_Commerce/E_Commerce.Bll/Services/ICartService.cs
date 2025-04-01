namespace E_Commerce.Bll.Services;

public interface ICartService
{
    Task AddProductToCardAsync(long customerId, long productId, int quantity)

}