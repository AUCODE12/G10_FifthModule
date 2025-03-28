using Microsoft.Identity.Client;

namespace CarRentalSystem.Dal.Entities;

public class Cart
{
    public long CartId { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<CartProduct> CartProducts { get; set; }
}
