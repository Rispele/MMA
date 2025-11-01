using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Domain.Models.Room;

public class Room
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public RoomScheduleAddress? ScheduleAddress { get; set; }
    public required RoomParameters Parameters { get; set; }
    public RoomAttachments Attachments { get; set; } = null!;
    public string? Owner { get; set; }
    public RoomFixInfo FixInfo { get; set; } = null!;
    public bool AllowBooking { get; set; }
    public List<Equipment.Equipment> Equipments { get; set; } = [];

    public void Update(
        string name,
        string? description,
        RoomParameters parameters,
        RoomAttachments attachments,
        string? owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        Name = name;
        Description = description;
        Parameters = parameters;
        Attachments = attachments;
        Owner = owner;
        FixInfo = fixInfo;
        AllowBooking = allowBooking;
    }
}