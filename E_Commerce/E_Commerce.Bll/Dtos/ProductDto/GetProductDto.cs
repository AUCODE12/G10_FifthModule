using E_Commerce.Dal.Entites;

namespace E_Commerce.Bll.Dtos.ProductDto;

public class GetProductDto
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageLink { get; set; }
}
