using E_Commerce.Dal;
using E_Commerce.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class ProductRepository : IProductRepository
{
    private readonly MainContext MainContext;

    public ProductRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public Task<long> IsertProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> SelectProductByIdAsync(long productId)
    {
        var product = await MainContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        return product;
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
