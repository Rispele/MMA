using Commons.ExternalClients.Booking.Models;

namespace Commons.ExternalClients.Booking;

public interface IBookingClient
{
    public Task<BookingResponse<RoomInfo[]>> GetAllRooms(CancellationToken cancellationToken);
    public Task<BookingResponse<FreeRoomInfo[]>> GetRoomsAvailableForBooking(GetFreeRoomsRequest request, CancellationToken cancellationToken);
    public Task<BookingResponse<long>> BookRoom(BookRoomRequest request, CancellationToken cancellationToken);
    public Task<BookingResponse> DeclineBooking(long eventId, CancellationToken cancellationToken);
    public Task<BookingResponse> ConfirmBooking(long eventId, CancellationToken cancellationToken);
}