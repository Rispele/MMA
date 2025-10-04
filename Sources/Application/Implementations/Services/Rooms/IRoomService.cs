using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Services.Rooms;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int id, CancellationToken cancellationToken);
    Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto request, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomRequest request, CancellationToken cancellationToken);
}
