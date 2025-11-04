using System.ComponentModel.DataAnnotations;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Dtos.Requests.Equipments;

public record PatchEquipmentDto
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public EquipmentSchemaDto Schema { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина инвентарного номера должна быть от 1 до 256 символов включительно")]
    public string? InventoryNumber { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина серийного номера должна быть от 1 до 256 символов включительно")]
    public string? SerialNumber { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина ip-адреса должна быть от 1 до 256 символов включительно")]
    public string? NetworkEquipmentIp { get; set; }

    [Length(minimumLength: 1, maximumLength: 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; set; }

    public EquipmentStatus? Status { get; set; }
}