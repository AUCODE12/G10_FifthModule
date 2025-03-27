using E_Commerce.Dal;
using E_Commerce.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Services;

public class ProductRepository : IProductRepository
{
    private readonly MainContext MainContext;

    public ProductRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public Task<long> InsertProductAsync(Product product)
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
