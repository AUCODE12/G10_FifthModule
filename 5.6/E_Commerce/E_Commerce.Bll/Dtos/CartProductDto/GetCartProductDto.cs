using E_Commerce.Bll.Dtos.ProductDto;

namespace E_Commerce.Bll.Dtos.CartProductDto;

public class GetCartProductDto
{
    public long CartProductId { get; set; }
    public int Quantity { get; set; }
    public long CartId { get; set; }
    public long ProductId { get; set; }
    public GetProductDto GetProductDto { get; set; }
}
