using System.ComponentModel.DataAnnotations;
using Rooms.Core.Implementations.Dtos.Equipment;
using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Domain.Models.Equipment;

namespace WebApi.Models.Requests;

public record CreateEquipmentModel
{
    public RoomDto? Room { get; init; }
    [Required]
    public EquipmentTypeDto Type { get; init; }
    [Required]
    public EquipmentSchemaDto Schema { get; init; }
    public string? InventoryNumber { get; init; }
    public string? SerialNumber { get; init; }
    public string? NetworkEquipmentIp { get; init; }
    public string? Comment { get; init; }
    public EquipmentStatus? Status { get; init; }
}