namespace Rooms.Core.Dtos.Requests.BookingRequests;

public record GetBookingRequestsDto(int BatchNumber, int BatchSize, int AfterBookingRequestId, BookingRequestsFilterDto? Filter);