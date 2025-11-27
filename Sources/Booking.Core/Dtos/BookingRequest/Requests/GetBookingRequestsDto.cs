namespace Rooms.Core.Dtos.BookingRequest.Requests;

public record GetBookingRequestsDto(int BatchNumber, int BatchSize, int AfterBookingRequestId, BookingRequestsFilterDto? Filter);