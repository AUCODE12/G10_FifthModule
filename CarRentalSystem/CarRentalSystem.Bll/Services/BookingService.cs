using CarRentalSystem.Bll.Dto;
using CarRentalSystem.Dal.Entities;
using CarRentalSystem.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Bll.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    public async Task<ICollection<BookingGetDto>> GetAllAsync()
    {
        var bookingQuery = bookingRepository.GetAll();

        var loadedBookings = await bookingQuery.ToListAsync();

        return null;
    }

    public async Task<long> PostAsync(BookingCreateDto booking)
    {
        var bookingEntity = new Booking()
        {
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            CustomerId = booking.CustomerId,
            CarId = booking.CarId
        };

        return await bookingRepository.AddAsync(bookingEntity);
    }
}
