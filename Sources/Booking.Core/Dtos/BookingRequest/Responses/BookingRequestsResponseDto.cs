namespace Booking.Core.Dtos.BookingRequest.Responses;

public record BookingRequestsResponseDto(BookingRequestDto[] BookingRequests, int Count, int? LastBookingRequestId);