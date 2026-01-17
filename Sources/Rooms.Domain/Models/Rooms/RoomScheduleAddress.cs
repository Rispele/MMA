namespace Rooms.Domain.Models.Rooms;

public record RoomScheduleAddress(
    string RoomNumber,
    string Address,
    int ScheduleRoomId);