using Booking.Core.Dtos.BookingRequest;
using Booking.Core.Dtos.BookingRequest.Requests;
using Booking.Core.Dtos.BookingRequest.Responses;

namespace Booking.Core.Services.Booking.BookingRequests.Interfaces;

public interface IBookingRequestService
{
    Task<BookingRequestDto> GetBookingRequestById(int bookingRequestId, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken);
    Task<BookingRequestsResponseDto> FilterBookingRequests(GetBookingRequestsDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> CreateBookingRequest(CreateBookingRequestDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> PatchBookingRequest(int bookingRequestId, PatchBookingRequestDto dto, CancellationToken cancellationToken);
}