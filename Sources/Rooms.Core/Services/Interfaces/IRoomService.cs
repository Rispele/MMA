using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;

namespace Rooms.Core.Services.Interfaces;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken);
    Task<RoomsResponseDto> FilterRooms(GetRoomsDto dto, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(CreateRoomDto dto, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken);
}
