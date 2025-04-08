using E_Commerce.PaymentBroker.Models;

namespace E_Commerce.PaymentBroker.Services;

public class PaymentService : IPaymentService
{
    public async Task ProcessPaymeTansaction(IPaymeTransactionRequestDto paymeTransactionRequestDto)
    {
        await Task.Delay(2000);
    }
}
