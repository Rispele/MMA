using AutoFixture;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;

namespace Commons.Tests.Helpers.SDK.Equipments.Parts;

public partial class EquipmentsSdk
{
    public async Task<EquipmentSchemaDto> CreateEquipmentSchema(
        string? name = null,
        Dictionary<string, string>? parameterValues = null,
        CancellationToken cancellationToken = default)
    {
        var type = await CreateEquipmentType(
            new CreateEquipmentTypeDto
            {
                Name = (name ?? Guid.NewGuid().ToString("N")) + "type",
                Parameters = (parameterValues ?? [])
                    .Select(t => t.Key)
                    .Select(t => new EquipmentParameterDescriptorDto
                    {
                        Name = t,
                        Required = true
                    })
                    .ToArray()
            },
            cancellationToken: cancellationToken);

        return await CreateEquipmentSchema(type.Id, name, parameterValues, cancellationToken: cancellationToken);
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
        var request = new GetEquipmentSchemasDto(batchNumber, batchSize, filter);

        return equipmentSchemaService.FilterEquipmentSchemas(request, cancellationToken);
    }
}