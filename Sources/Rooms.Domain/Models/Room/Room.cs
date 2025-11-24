using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Domain.Models.Room;

[GenerateFieldNames]
public class Room
{
    private readonly List<Equipment> equipments = null!;
    private int? id;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private Room()
    {
    }

    public Room(
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

        equipments = [];
    }

    public int Id => id ?? throw new InvalidOperationException("Not initialized yet");
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public RoomScheduleAddress? ScheduleAddress { get; private set; }
    public RoomParameters Parameters { get; private set; } = null!;
    public RoomAttachments Attachments { get; private set; } = null!;
    public string? Owner { get; private set; }
    public RoomFixInfo FixInfo { get; private set; } = null!;
    public bool AllowBooking { get; private set; }

    public IEnumerable<Equipment> Equipments => equipments;

    public int? OperatorDepartmentId { get; [UsedImplicitly] private set; }

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

    public void SetScheduleAddress(string number, string address)
    {
        ScheduleAddress = new RoomScheduleAddress(number, address);
    }

    internal void SetId(int idToSet)
    {
        id = idToSet;
    }
}