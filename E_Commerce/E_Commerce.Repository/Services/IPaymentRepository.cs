using E_Commerce.Dal.Entites;

namespace E_Commerce.Repository.Services;

public interface IPaymentRepository
{
    Task<long> InsertPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
}