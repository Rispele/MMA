using WebApi.Core.Models.Room;

namespace WebApi.Core.Models.OperatorDepartments;

public class OperatorDepartmentRoomInfoModel
{
    public int RoomId { get; set; }
    public ScheduleAddressModel? ScheduleAddress { get; set; }
}
