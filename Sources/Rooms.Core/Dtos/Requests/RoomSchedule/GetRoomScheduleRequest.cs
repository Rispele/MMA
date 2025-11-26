using Rooms.Core.Dtos.Room;

namespace Rooms.Core.Dtos.Requests.RoomSchedule;

public record GetRoomScheduleRequest(ScheduleAddressDto ScheduleAddress, DateOnly FromDate, DateOnly ToDate);