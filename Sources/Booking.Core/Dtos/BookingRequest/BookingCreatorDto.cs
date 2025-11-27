namespace Booking.Core.Dtos.BookingRequest;

public class BookingCreatorDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}