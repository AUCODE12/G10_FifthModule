namespace E_Commerce.Dal.Entites;

public class OrderStatus
{
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public List<Order> Orders { get; set; }
}