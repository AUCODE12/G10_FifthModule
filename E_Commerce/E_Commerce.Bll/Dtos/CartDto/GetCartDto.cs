using E_Commerce.Dal.Entites;

namespace E_Commerce.Bll.Dtos.CartDto;

public class GetCartDto
{
    public long CartId { get; set; }
    public long CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CartProduct> GetCartProductDtos { get; set; }
}
