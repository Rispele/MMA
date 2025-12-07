using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;
using Rooms.Domain.Propagated.Equipments;

namespace Rooms.Domain.Models.Equipments;

[GenerateFieldNames]
internal class Equipment
{
    private readonly int? id;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private Equipment()
    {
    }

    public Equipment(
        int roomId,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        RoomId = roomId;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }

    public int Id => id ?? throw new InvalidOperationException("Equipment id not initialized yet");
    public int RoomId { get; private set; }
    public EquipmentSchema Schema { get; private set; } = null!;
    public string? InventoryNumber { get; private set; }
    public string? SerialNumber { get; private set; }
    public string? NetworkEquipmentIp { get; private set; }
    public string? Comment { get; private set; }
    public EquipmentStatus? Status { get; private set; }

    public void Update(
        int roomId,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status)
    {
        RoomId = roomId;
        Schema = schema;
        InventoryNumber = inventoryNumber;
        SerialNumber = serialNumber;
        NetworkEquipmentIp = networkEquipmentIp;
        Comment = comment;
        Status = status;
    }

    #region For Tests

    /// <summary>
    /// Use only for tests, ORM handles id initialization
    /// </summary>
    internal Equipment(
        int id,
        int roomId,
        EquipmentSchema schema,
        string? inventoryNumber,
        string? serialNumber,
        string? networkEquipmentIp,
        string? comment,
        EquipmentStatus? status) : this(roomId, schema, inventoryNumber, serialNumber, networkEquipmentIp, comment, status)
    {
        this.id = id;
    }

    #endregion
}