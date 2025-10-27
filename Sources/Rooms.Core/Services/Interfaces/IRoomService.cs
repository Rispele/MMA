using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Services.Interfaces;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken);
    Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto requestDto, CancellationToken cancellationToken);
    Task<RoomDto> CreateRoom(CreateRoomDto dto, CancellationToken cancellationToken);
    Task<RoomDto> UpdateWithEquipment(int roomId, Equipment equipment, CancellationToken cancellationToken);
    Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken);
}