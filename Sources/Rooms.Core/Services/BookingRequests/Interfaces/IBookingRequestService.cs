using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.BookingRequests.Interfaces;

public interface IBookingRequestService
{
    Task<BookingRequestDto> GetBookingRequestById(int bookingRequestId, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken);
    Task<BookingRequestsResponseDto> FilterBookingRequests(GetBookingRequestsDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> CreateBookingRequest(CreateBookingRequestDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> PatchBookingRequest(int bookingRequestId, PatchBookingRequestDto dto, CancellationToken cancellationToken);
}