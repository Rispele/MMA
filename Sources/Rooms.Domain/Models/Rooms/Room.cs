using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Models.Rooms.Fix;
using Rooms.Domain.Models.Rooms.Parameters;

namespace Rooms.Domain.Models.Rooms;

[GenerateFieldNames]
public class Room
{
    private readonly List<Equipment> equipments = null!;
    private readonly int? id;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private Room()
    {
    }

    public Room(
        string name,
        string? description,
        RoomScheduleAddress scheduleAddress,
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
    public OperatorDepartment? OperatorDepartment { get; private set; }

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

    public void SetScheduleAddress(string number, string address, int scheduleRoomId)
    {
        ScheduleAddress = new RoomScheduleAddress(number, address, scheduleRoomId);
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    internal Room(
        int id,
        string name,
        string? description,
        RoomScheduleAddress scheduleAddress,
        RoomParameters parameters,
        RoomAttachments attachments,
        string? owner,
        RoomFixInfo fixInfo,
        bool allowBooking) : this(name, description, scheduleAddress, parameters, attachments, owner, fixInfo, allowBooking)
    {
        this.id = id;
    }

    #endregion
}