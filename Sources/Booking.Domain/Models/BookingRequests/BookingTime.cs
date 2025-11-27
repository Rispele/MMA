using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Domain.Models.BookingRequests;

public record BookingTime
{
    public required string RoomId { get; init; }
    public required DateOnly Date { get; init; }
    public required TimeOnly TimeFrom { get; init; }
    public required TimeOnly TimeTo { get; init; }
    public required BookingFrequency Frequency { get; init; }
    public required DateOnly? BookingFinishDate { get; init; }
}