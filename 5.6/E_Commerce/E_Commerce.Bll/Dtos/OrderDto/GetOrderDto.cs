using E_Commerce.Bll.Dtos.OrderProductDto;
using E_Commerce.Bll.Dtos.OrderStatusDto;
using E_Commerce.Dal.Entities;

namespace E_Commerce.Bll.Dtos.OrderDto;

public class GetOrderDto
{
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public byte DiscountPercentage { get; set; }
    public decimal ServicePrice { get; set; }
    public string OrderStatus { get; set; }
    public List<GetOrderProductDto> GetOrderProductDtos { get; set; }
}
