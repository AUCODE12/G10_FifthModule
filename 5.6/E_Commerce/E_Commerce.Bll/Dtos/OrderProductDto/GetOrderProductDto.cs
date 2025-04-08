using E_Commerce.Bll.Dtos.ProductDto;
using E_Commerce.Dal.Entities;

namespace E_Commerce.Bll.Dtos.OrderProductDto;

public class GetOrderProductDto
{
    public long OrderProductId { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public GetProductDto GetProductDto { get; set; }
}
