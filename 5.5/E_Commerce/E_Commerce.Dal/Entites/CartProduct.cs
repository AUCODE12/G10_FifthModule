namespace E_Commerce.Dal.Entites;

public class CartProduct
{
    public long CartProductId { get; set; }
    public int Quantity { get; set; }
    public long CartId { get; set; }
    public Cart Cart { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
}
