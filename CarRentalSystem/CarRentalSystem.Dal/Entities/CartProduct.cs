namespace CarRentalSystem.Dal.Entities;

public class CartProduct
{
    public long ProductId { get; set; }
    public Product Product { get; set; }


    public long CartId { get; set; }
    public Cart Cart { get; set; }

    public int Quentity { get; set; }
}
