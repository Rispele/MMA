using Rooms.Core.Dtos.BookingRequest;

namespace Rooms.Core.Dtos.Responses;

public record BookingRequestsResponseDto(BookingRequestDto[] BookingRequests, int Count, int? LastBookingRequestId);