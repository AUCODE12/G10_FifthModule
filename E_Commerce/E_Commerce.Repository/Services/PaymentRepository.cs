using E_Commerce.Dal.Entites;

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
