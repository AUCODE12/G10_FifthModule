using CarRentalSystem.Dal.Enums;

namespace CarRentalSystem.Dal.Entities;

public class Booking
{
    public long BookingId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalCost { get; set; }

    public BookingStatus BookingStatus { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    public long CarId { get; set; }
    public Car Car { get; set; }

    public ICollection<Payment> Payments { get; set; }
}
