using AutoFixture;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;

namespace Rooms.Tests.Helpers.SDK.Equipments.Parts;

public partial class EquipmentsSdk
{
    public async Task<EquipmentDto> CreateEquipment(int roomId, CancellationToken cancellationToken = default)
    {
        var fixture = new Fixture();

        var equipmentType = await CreateEquipmentType(cancellationToken: cancellationToken);
        var equipmentSchema = await CreateEquipmentSchema(equipmentType.Id, cancellationToken: cancellationToken);
            
        var createRequest = fixture
            .Build<CreateEquipmentDto>()
            .With(create => create.SchemaId, equipmentSchema.Id)
            .With(create => create.RoomId, roomId)
            .Create();
        
        return await CreateEquipment(createRequest, cancellationToken);
    }
    
    public Task<EquipmentDto> CreateEquipment(
        CreateEquipmentDto createRequest,
        CancellationToken cancellationToken = default)
    {
        return equipmentService.CreateEquipment(createRequest, cancellationToken);
    }

    public async Task<EquipmentDto> PatchEquipment(
        int equipmentId,
        PatchEquipmentDto patchRequest,
        CancellationToken cancellationToken = default)
    {
        return await equipmentService.PatchEquipment(equipmentId, patchRequest, cancellationToken);
    }

    public Task<EquipmentDto> GetEquipment(int equipmentId, CancellationToken cancellationToken = default)
    {
        return equipmentService.GetEquipmentById(equipmentId, cancellationToken);
    }

    public Task<EquipmentsResponseDto> FilterEquipments(
        int batchNumber = 0,
        int batchSize = 100,
        EquipmentsFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetEquipmentsDto(batchNumber, batchSize, -1, filter);
        
        return equipmentService.FilterEquipments(request, cancellationToken);
    }
}