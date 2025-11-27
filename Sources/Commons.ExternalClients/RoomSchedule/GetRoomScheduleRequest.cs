namespace Rooms.Core.Clients.RoomSchedule;

public record GetRoomScheduleRequest(
    string RoomNumber,
    string RoomAddress,
    DateOnly FromDate,
    DateOnly ToDate);