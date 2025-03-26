using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface IProductRepository
{
    Task<Product> GetProductByProductIdAsync(long productId);
}