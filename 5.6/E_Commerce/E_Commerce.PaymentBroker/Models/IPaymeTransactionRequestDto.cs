using E_Commerce.Dal.Entities;

namespace E_Commerce.PaymentBroker.Models;

public class IPaymeTransactionRequestDto
{
    public decimal TotalPrice { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public Card Card { get; set; }

}
