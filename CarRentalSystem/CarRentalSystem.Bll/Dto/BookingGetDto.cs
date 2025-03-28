using CarRentalSystem.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Bll.Dto;

public class BookingGetDto
{
    public long BookingId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalCost { get; set; }

    public long CustomerId { get; set; }
    public string CustomerName { get; set; }

    public long CarId { get; set; }
    public string CarModel { get; set; }

    public ICollection<PaymentGetDto> Payments { get; set; }
}
