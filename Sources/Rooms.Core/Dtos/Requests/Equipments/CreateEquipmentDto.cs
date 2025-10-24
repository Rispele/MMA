using System.ComponentModel.DataAnnotations;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Dtos.Requests.Equipments;

public record CreateEquipmentDto
{
    [Required]
    public RoomDto Room { get; set; } = default!;

    [Required]
    public EquipmentSchemaDto SchemaDto { get; set; }

    [Length(1, 256, ErrorMessage = "Длина инвентарного номера должна быть от 1 до 256 символов включительно")]
    public string? InventoryNumber { get; set; } = default!;

    [Length(1, 256, ErrorMessage = "Длина серийного номера должна быть от 1 до 256 символов включительно")]
    public string? SerialNumber { get; set; } = default!;

    [Length(1, 256, ErrorMessage = "Длина ip-адреса должна быть от 1 до 256 символов включительно")]
    public string? NetworkEquipmentIp { get; set; } = default!;

    [Length(1, 256, ErrorMessage = "Длина комментария должна быть от 1 до 256 символов включительно")]
    public string? Comment { get; set; }

    public EquipmentStatus? Status { get; set; } = default!;
}