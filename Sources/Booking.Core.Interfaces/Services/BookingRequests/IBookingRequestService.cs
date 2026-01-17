using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Interfaces.Dtos.BookingRequest.Responses;
using Commons.ExternalClients.Booking.Models;

namespace Booking.Core.Interfaces.Services.BookingRequests;

public interface IBookingRequestService
{
    Task<BookingRequestDto> GetBookingRequestById(int bookingRequestId, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken);
    Task<BookingRequestsResponseDto> FilterBookingRequests(GetBookingRequestsDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> CreateBookingRequest(CreateBookingRequestDto dto, CancellationToken cancellationToken);
    Task<BookingRequestDto> PatchBookingRequest(int bookingRequestId, PatchBookingRequestDto dto, CancellationToken cancellationToken);
    Task<FreeRoomInfo[]?> GetAvailableForBookingRooms(GetFreeRoomsRequest dto, CancellationToken cancellationToken);
}