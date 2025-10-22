using Rooms.Core.Dtos.Room;

namespace Rooms.Core.Dtos.Responses;

public record RoomsResponseDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);
