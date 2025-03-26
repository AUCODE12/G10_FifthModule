namespace E_Commerce.Bll.Services;

public interface ICartService
{
    Task AddProductToCartAsync(long customerId, long ProductId, int quantity);
    Task ClearCartAsync(long customerId);

}