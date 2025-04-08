using E_Commerce.PaymentBroker.Models;

namespace E_Commerce.PaymentBroker.Services;

public interface IPaymentService
{
    Task ProcessPaymeTansaction(IPaymeTransactionRequestDto paymeTransactionRequestDto);
}
