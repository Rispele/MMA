namespace Booking.Core.Interfaces.Dtos.BookingRequest.Responses;

public record BookingRequestsResponseDto(BookingRequestDto[] BookingRequests, int TotalCount);