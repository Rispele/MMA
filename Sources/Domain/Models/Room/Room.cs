using Domain.Models.Room.Fix;
using Domain.Models.Room.Parameters;

namespace Domain.Models.Room;

public class Room
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public RoomScheduleAddress ScheduleAddress { get; private set; }
    public RoomParameters Parameters { get; private set; }
    public RoomAttachments Attachments { get; private set; }
    public string Owner { get; private set; }
    public RoomFixInfo FixInfo { get; private set; }
    public bool AllowBooking { get; private set; }

    public Room(
        int id,
        string name,
        string description,
        RoomScheduleAddress scheduleAddress,
        RoomParameters parameters, 
        RoomAttachments attachments,
        string owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        Id = id;
        Name = name;
        Description = description;
        ScheduleAddress = scheduleAddress;
        Parameters = parameters;
        Attachments = attachments;
        FixInfo = fixInfo;
        Owner = owner;
        AllowBooking = allowBooking;
    }
}