using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dal.Entities;

public class OrderStatus
{
    [Key]
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public List<Order> Orders { get; set; }
}
