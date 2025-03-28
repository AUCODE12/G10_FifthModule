namespace CarRentalSystem.Dal.Entities;

public class Review
{
    public long ReviewId { get; set; }
    public int? Rating { get; set; }
    public string? Comment { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    public long CarId { get; set; }
    public Car Car { get; set; }
}
