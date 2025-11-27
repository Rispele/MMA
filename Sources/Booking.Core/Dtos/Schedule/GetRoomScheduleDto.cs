namespace Rooms.Core.Dtos.Room.Requests;

public record GetRoomScheduleDto(int RoomId, DateOnly From, DateOnly To);