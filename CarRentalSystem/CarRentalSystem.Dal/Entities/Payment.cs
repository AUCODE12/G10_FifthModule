using CarRentalSystem.Dal.Enums;

namespace CarRentalSystem.Dal.Entities;

public class Payment
{
    public long PaymentId { get; set; }
    public double Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    public long BookingId { get; set; }
    public Booking Booking { get; set; }
}
