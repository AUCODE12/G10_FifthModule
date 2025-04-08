namespace E_Commerce.Bll.Dtos.CustomerDto;

public class GetCustomerDto
{
    public long CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
