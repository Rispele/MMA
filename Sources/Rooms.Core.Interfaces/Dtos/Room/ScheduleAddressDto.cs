namespace Rooms.Core.Interfaces.Dtos.Room;

public class ScheduleAddressDto
{
    public string RoomNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int ScheduleRoomId { get; set; }
}