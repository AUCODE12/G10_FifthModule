using E_Commerce.Dal;
using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly MainContext MainContext;

    public OrderProductRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task InsertOrderProductsAsync(List<OrderProduct> orderProducts)
    {
        await MainContext.AddRangeAsync(orderProducts);
        await MainContext.SaveChangesAsync();
    }
}
