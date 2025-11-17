using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;

namespace Rooms.Domain.Models.Room;

/// <summary>
/// Аудитория
/// </summary>
[GenerateFieldNames]
public class Room
{
    private int? id = null;
    private readonly List<Equipment> equipments = null!;

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

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id => id ?? throw new InvalidOperationException();

    /// <summary>
    /// Название аудитории
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Описание аудитории
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Расположение аудитории
    /// </summary>
    public RoomScheduleAddress? ScheduleAddress { get; private set; }

    /// <summary>
    /// Параметры аудитории
    /// </summary>
    public RoomParameters Parameters { get; private set; } = null!;

    /// <summary>
    /// Файлы-приложения
    /// </summary>
    public RoomAttachments Attachments { get; private set; } = null!;

    /// <summary>
    /// Владелец аудитории
    /// </summary>
    public string? Owner { get; private set; }

    /// <summary>
    /// Статусная модель аудитории
    /// </summary>
    public RoomFixInfo FixInfo { get; private set; } = null!;

    /// <summary>
    /// Доступна для бронирования
    /// </summary>
    public bool AllowBooking { get; private set; }

    /// <summary>
    /// Привязанные единицы оборудования
    /// </summary>
    public IReadOnlyList<Equipment> Equipments => equipments;

    /// <summary>
    /// Идентификатор операторской
    /// </summary>
    public int? OperatorDepartmentId { get; [UsedImplicitly] private set; }

    public void Update(
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
        Owner = owner;
        FixInfo = fixInfo;
        AllowBooking = allowBooking;
    }
}