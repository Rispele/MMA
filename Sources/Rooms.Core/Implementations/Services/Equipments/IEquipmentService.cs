using Rooms.Core.Implementations.Dtos.Equipment;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentCreating;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentPatching;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;
using Rooms.Core.Implementations.Dtos.Responses;

namespace Rooms.Core.Implementations.Services.Equipments;

public interface IEquipmentService
{
    Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken);
    Task<EquipmentsBatchDto> FilterEquipments(GetEquipmentsRequestDto request, CancellationToken cancellationToken);
    Task<EquipmentDto> CreateEquipment(CreateEquipmentRequest request, CancellationToken cancellationToken);
    Task<EquipmentDto> PatchEquipment(int equipmentId, PatchEquipmentRequest request, CancellationToken cancellationToken);
}