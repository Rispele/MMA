using System.ComponentModel.DataAnnotations;
using Rooms.Core.Dtos.Equipment;
using Rooms.Domain.Models.Equipment;

namespace WebApi.Models.Requests.Equipments;

public record PatchEquipmentModel
{
    public int? RoomId { get; init; }

    [Required]
    public EquipmentSchemaDto Schema { get; init; }

    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}