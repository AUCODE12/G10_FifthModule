namespace CarRentalSystem.Dal.Entities;

public class Product
{
    public long ProductId { get; set; }
    public string Name { get; set; }

    public ICollection<CartProduct> CartProducts { get; set; }
}
