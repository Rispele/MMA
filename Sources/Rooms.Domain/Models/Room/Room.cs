using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace Rooms.Domain.Models.Room;

public class Room
{
    public Room()
    {
    }

    public Room(
        string name,
        string? description,
        RoomScheduleAddress? scheduleAddress,
        RoomParameters parameters,
        RoomAttachments attachments,
        string? owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        Name = name;
        Description = description;
        ScheduleAddress = scheduleAddress;
        Parameters = parameters;
        Attachments = attachments;
        FixInfo = fixInfo;
        Owner = owner;
        AllowBooking = allowBooking;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public RoomScheduleAddress? ScheduleAddress { get; set; }
    public RoomParameters Parameters { get; set; } = null!;
    public RoomAttachments Attachments { get; set; } = null!;
    public string? Owner { get; set; }
    public RoomFixInfo FixInfo { get; set; } = null!;
    public bool AllowBooking { get; set; }
    public List<Equipment.Equipment> Equipments { get; set; } = [];

    public static Room New(
        string name,
        string? description,
        RoomParameters parameters,
        RoomAttachments attachments,
        string? owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        return new Room(name, description, null, parameters, attachments, owner, fixInfo, allowBooking);
    }

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