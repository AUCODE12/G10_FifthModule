using CarRentalSystem.Dal.Entities;

namespace CarRentalSystem.Repository.Services;

public interface IBookingRepository
{
    Task<long> AddAsync(Booking booking);
    IQueryable<Booking> GetAll();
}