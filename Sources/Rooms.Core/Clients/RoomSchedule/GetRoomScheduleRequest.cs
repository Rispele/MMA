using Rooms.Core.Dtos.Room;

namespace Rooms.Core.Clients.RoomSchedule;

public record GetRoomScheduleRequest(ScheduleAddressDto ScheduleAddress, DateOnly FromDate, DateOnly ToDate);