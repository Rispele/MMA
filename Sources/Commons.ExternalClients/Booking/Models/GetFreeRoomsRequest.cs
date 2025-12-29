namespace Commons.ExternalClients.Booking.Models;

public record GetFreeRoomsRequest(
    DateOnly Date,
    TimeOnly BeginTime,
    TimeOnly EndTime);