using Application.Implementations.Dtos.Room;

namespace Application.Implementations.Dtos.Responses;

public record RoomsResponseDto(
    RoomDto[] Rooms,
    int Count,
    int? LastRoomId);
