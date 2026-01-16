using AutoFixture;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;

namespace Commons.Tests.Helpers.SDK.Equipments.Parts;

public partial class EquipmentsSdk
{
    public Task<EquipmentTypeDto> CreateEquipmentType(
        string name,
        CancellationToken cancellationToken = default)
    {
        return equipmentTypeService.CreateEquipmentType(
            new CreateEquipmentTypeDto
            {
                Name = name,
                Parameters = []
            },
            cancellationToken);
    }

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
        var request = new GetEquipmentTypesDto(batchNumber, batchSize, filter);

        return equipmentTypeService.FilterEquipmentTypes(request, cancellationToken);
    }
}