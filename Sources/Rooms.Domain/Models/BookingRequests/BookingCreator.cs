namespace Rooms.Domain.Models.BookingRequests;

public record BookingCreator
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
}