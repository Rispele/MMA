using AutoFixture;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Tests.Helpers.SDK.Equipments.Parts;

public partial class EquipmentsSdk
{
    public Task<EquipmentTypeDto> CreateEquipmentType(
        CreateEquipmentTypeDto? createRequest = null,
        CancellationToken cancellationToken = default)
    {
        createRequest ??= new Fixture().Create<CreateEquipmentTypeDto>();
        return equipmentTypeService.CreateEquipmentType(createRequest, cancellationToken);
    }

    public async Task<EquipmentTypeDto> PatchEquipmentType(
        int equipmentTypeId,
        PatchEquipmentTypeDto patchRequest,
        CancellationToken cancellationToken = default)
    {
        return await equipmentTypeService.PatchEquipmentType(equipmentTypeId, patchRequest, cancellationToken);
    }

    public Task<EquipmentTypeDto> GetEquipmentType(int equipmentTypeId, CancellationToken cancellationToken = default)
    {
        return equipmentTypeService.GetEquipmentTypeById(equipmentTypeId, cancellationToken);
    }

    public Task<EquipmentTypesResponseDto> FilterEquipmentTypes(
        int batchNumber = 0,
        int batchSize = 100,
        EquipmentTypesFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetEquipmentTypesDto(batchNumber, batchSize, -1, filter);
        
        return equipmentTypeService.FilterEquipmentTypes(request, cancellationToken);
    }
}