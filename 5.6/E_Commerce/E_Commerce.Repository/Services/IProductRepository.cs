using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface IProductRepository
{
    Task<long> IsertProductAsync(Product product);
    Task<Product> SelectProductByIdAsync(long productId);  
    Task UpdateProductAsync(Product product);
}