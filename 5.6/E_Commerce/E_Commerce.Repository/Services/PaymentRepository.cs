using E_Commerce.Dal.Entities;

namespace E_Commerce.Repository.Services;

public class PaymentRepository : IPaymentRepository
{
    public Task<long> InsertPaymentAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePaymentAsync(Payment payment)
    {
        throw new NotImplementedException();
    }
}
