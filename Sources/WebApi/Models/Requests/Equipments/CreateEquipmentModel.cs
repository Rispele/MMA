using System.ComponentModel.DataAnnotations;
using Rooms.Domain.Models.Equipment;
using WebApi.Models.Equipment;
using WebApi.Models.Room;

namespace WebApi.Models.Requests.Equipments;

public record CreateEquipmentModel
{
    public RoomModel RoomModel { get; init; } = default!;
    [Required]
    public EquipmentTypeModel TypeModel { get; init; }
    [Required]
    public EquipmentSchemaModel SchemaModel { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}