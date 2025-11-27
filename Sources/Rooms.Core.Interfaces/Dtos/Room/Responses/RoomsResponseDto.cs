namespace Rooms.Core.Interfaces.Dtos.Room.Responses;

public record RoomsResponseDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);