using Rooms.Core.Interfaces.Dtos.Room;

namespace Rooms.Core.Interfaces.Dtos.OperatorDepartments;

public class OperatorDepartmentRoomInfoDto
{
    public int RoomId { get; set; }
    public ScheduleAddressDto? ScheduleAddress { get; set; }
}
