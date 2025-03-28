using CarRentalSystem.Bll.Dto;

namespace CarRentalSystem.Bll.Services;

public interface IBookingService
{
    Task<long> PostAsync(BookingCreateDto booking);
    Task<ICollection<BookingGetDto>> GetAllAsync();
}
