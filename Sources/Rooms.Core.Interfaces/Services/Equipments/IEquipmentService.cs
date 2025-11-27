using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;

namespace Rooms.Core.Interfaces.Services.Equipments;

public interface IEquipmentService
{
    Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken);
    Task<EquipmentsResponseDto> FilterEquipments(GetEquipmentsDto dto, CancellationToken cancellationToken);
    Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken);
    Task<EquipmentDto> PatchEquipment(int equipmentId, PatchEquipmentDto dto, CancellationToken cancellationToken);
}