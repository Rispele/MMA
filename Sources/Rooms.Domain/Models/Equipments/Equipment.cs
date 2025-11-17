namespace Rooms.Domain.Models.Equipments;

/// <summary>
/// Единица оборудования
/// </summary>
public class Equipment
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор связанной аудитории
    /// </summary>
    public required int RoomId { get; set; }

    /// <summary>
    /// Модель оборудования
    /// </summary>
    public required EquipmentSchema Schema { get; set; } = null!;

    /// <summary>
    /// Инвентарный номер
    /// </summary>
    public string? InventoryNumber { get; set; }

    /// <summary>
    /// Серийный номер
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// IP сетевого оборудования
    /// </summary>
    public string? NetworkEquipmentIp { get; set; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Статус оборудования
    /// </summary>
    public EquipmentStatus? Status { get; set; }

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
}