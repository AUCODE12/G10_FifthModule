namespace CarRentalSystem.Bll.Dto;

public class BookingCreateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public long CustomerId { get; set; }

    public long CarId { get; set; }
}
