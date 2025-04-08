using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public interface IOrderProductRepository
{
    Task InsertOrderProductsAsync(List<OrderProduct> orderProducts);
}