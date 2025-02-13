namespace CarRentalSystem.Dal.Entities;

public class Car
{
    public long CarId { get; set; }
    public string Model { get; set; }
    public string  Brand  { get; set; }
    public DateTime CreatedYear { get; set; }
    public double PricePerDay { get; set; }

    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
