using AutoFixture;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Equipment.Responses;

namespace Rooms.Tests.Helpers.SDK.Equipments.Parts;

public partial class EquipmentsSdk
{
    public async Task<EquipmentSchemaDto> CreateEquipmentSchema(string? name = null, CancellationToken cancellationToken = default)
    {
        var type = await CreateEquipmentType(cancellationToken: cancellationToken);
        
        return await CreateEquipmentSchema(type.Id, name, cancellationToken: cancellationToken);
    }
    
    public Task<EquipmentSchemaDto> CreateEquipmentSchema(
        int typeId,
        string? name = null,
        Dictionary<string, string>? parameterValues = null,
        CancellationToken cancellationToken = default)
    {
        var createRequestBuilder = new Fixture()
            .Build<CreateEquipmentSchemaDto>()
            .With(create => create.EquipmentTypeId, typeId);

        if (name != null)
        {
            createRequestBuilder = createRequestBuilder.With(create => create.Name, name);
        }

        if (parameterValues != null)
        {
            createRequestBuilder = createRequestBuilder.With(create => create.ParameterValues, parameterValues);
        }
        
        return CreateEquipmentSchema(createRequestBuilder.Create(), cancellationToken);
    }
    
    public Task<EquipmentSchemaDto> CreateEquipmentSchema(
        CreateEquipmentSchemaDto createRequest,
        CancellationToken cancellationToken = default)
    {
        return equipmentSchemaService.CreateEquipmentSchema(createRequest, cancellationToken);
    }

    public async Task<EquipmentSchemaDto> PatchEquipmentSchema(
        int equipmentSchemaId,
        PatchEquipmentSchemaDto patchRequest,
        CancellationToken cancellationToken = default)
    {
        return await equipmentSchemaService.PatchEquipmentSchema(equipmentSchemaId, patchRequest, cancellationToken);
    }

    public Task<EquipmentSchemaDto> GetEquipmentSchema(int equipmentSchemaId, CancellationToken cancellationToken = default)
    {
        return equipmentSchemaService.GetEquipmentSchemaById(equipmentSchemaId, cancellationToken);
    }

    public Task<EquipmentSchemasResponseDto> FilterEquipmentSchemas(
        int batchNumber = 0,
        int batchSize = 100,
        EquipmentSchemasFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetEquipmentSchemasDto(batchNumber, batchSize, -1, filter);
        
        return equipmentSchemaService.FilterEquipmentSchemas(request, cancellationToken);
    }
}