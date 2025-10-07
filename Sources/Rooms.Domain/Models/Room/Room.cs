using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace Rooms.Domain.Models.Room;

[EntityTypeConfiguration<RoomEntityTypeConfiguration, Room>]
public class Room
{
    public int Id { get; [UsedImplicitly] private set; }
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public RoomScheduleAddress? ScheduleAddress { get; private set; }
    public RoomParameters Parameters { get; private set; } = null!;
    public RoomAttachments Attachments { get; private set; } = null!;
    public string? Owner { get; private set; }
    public RoomFixInfo FixInfo { get; private set; } = null!;
    public bool AllowBooking { get; private set; }

    [UsedImplicitly]
    protected Room()
    {
    }

    private Room(
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

    public static Room New(
        string name,
        string? description,
        RoomParameters parameters,
        RoomAttachments attachments,
        string? owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        return new Room(name, description, scheduleAddress: null, parameters, attachments, owner, fixInfo, allowBooking);
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