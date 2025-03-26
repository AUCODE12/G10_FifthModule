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

    public async Task<Product> GetProductByProductIdAsync(long productId)
    {
        var product = await MainContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        return product;
    }   
}
