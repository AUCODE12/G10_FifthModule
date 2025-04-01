using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dal.Entities;

public class CartProduct
{
    [Key]
    public long CartProductId { get; set; }
    public int Quantity { get; set; }
    public long CartId { get; set; }
    public Cart Cart { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
}
