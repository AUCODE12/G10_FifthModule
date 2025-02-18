using CarRentalSystem.Dal.Entities;
using Instagram.Dal;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repository.Services;

public class BookingRepository : IBookingRepository
{
    private readonly MainContext mainContext;

    public BookingRepository(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task<long> AddAsync(Booking booking)
    {
        mainContext.Entry(booking).State = EntityState.Added;
        await mainContext.SaveChangesAsync();
        return booking.BookingId;
        //mainContext.Bookings.Add(booking);
    }

    public IQueryable<Booking> GetAll()
    {
        var bookings = mainContext.Bookings;
        return bookings;
    }
}
