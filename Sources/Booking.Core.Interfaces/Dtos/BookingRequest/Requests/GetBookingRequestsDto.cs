namespace Booking.Core.Interfaces.Dtos.BookingRequest.Requests;

public record GetBookingRequestsDto(int BatchNumber, int BatchSize, BookingRequestsFilterDto? Filter);