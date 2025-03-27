using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface IProductRepository
{
    Task<long> InsertProductAsync(Product product);
    Task<Product> SelectProductByIdAsync(long productId);
    Task UpdateProductAsync(Product product);
}