using Rooms.Core.Implementations.Dtos.Equipment;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentCreating;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentPatching;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;
using Rooms.Core.Implementations.Dtos.Responses;

namespace Rooms.Core.Implementations.Services.Equipments;

public class EquipmentService : IEquipmentService
{
    public Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentsBatchDto> FilterEquipments(GetEquipmentRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentDto> CreateRoom(CreateEquipmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentDto> PatchEquipment(int equipmentId, PatchEquipmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}