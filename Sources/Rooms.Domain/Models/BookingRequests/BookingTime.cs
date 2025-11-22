namespace Rooms.Domain.Models.BookingRequests;

public class BookingTime
{
    public string RoomId { get; set; } = null!;
    public DateOnly Date { get; set; }
    public TimeOnly TimeFrom { get; set; }
    public TimeOnly TimeTo { get; set; }
    public BookingFrequency Frequency { get; set; }
    public DateOnly? BookingFinishDate { get; set; }
}