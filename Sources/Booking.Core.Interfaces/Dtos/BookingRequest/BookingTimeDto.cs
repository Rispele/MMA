using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest;

public class BookingTimeDto
{
    public int RoomId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly TimeFrom { get; set; }
    public TimeOnly TimeTo { get; set; }
    public BookingFrequency Frequency { get; set; }
    public DateOnly? BookingFinishDate { get; set; }
}