using CarRentalSystem.Bll.Dto;
using CarRentalSystem.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers;

[Route("api/booking")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService bookingService;

    public BookingController(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    [HttpPost("add")]
    public async Task<long> PostBooking(BookingCreateDto bookingCreateDto)
    {
        return await bookingService.PostAsync(bookingCreateDto);
    }
}
