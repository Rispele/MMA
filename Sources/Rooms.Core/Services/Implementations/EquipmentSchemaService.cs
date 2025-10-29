using Commons;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipment;
using EquipmentSchemaDtoConverter = Rooms.Core.DtoConverters.EquipmentSchemaDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class EquipmentSchemaService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEquipmentSchemaQueryFactory equipmentSchemaQueryFactory,
    IEquipmentTypeService equipmentTypeService) : IEquipmentSchemaService
{
    public async Task<EquipmentSchemaDto> GetEquipmentSchemaById(int equipmentSchemaId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentSchema = await GetEquipmentSchemaByIdInner(equipmentSchemaId, cancellationToken, context);

        return equipmentSchema.Map(EquipmentSchemaDtoConverter.Convert);
    }

    public async Task<EquipmentSchemasResponseDto> FilterEquipmentSchemas(GetEquipmentSchemasDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = equipmentSchemaQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentSchemaId, dto.Filter);

        var equipmentSchemas = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken);

        var convertedEquipmentSchemas = equipmentSchemas.Select(EquipmentSchemaDtoConverter.Convert).ToArray();
        int? lastEquipmentSchemaId = convertedEquipmentSchemas.Length == 0 ? null : convertedEquipmentSchemas.Select(t => t.Id).Max();

        return new EquipmentSchemasResponseDto(convertedEquipmentSchemas, convertedEquipmentSchemas.Length, lastEquipmentSchemaId);
    }

    public async Task<EquipmentSchemaDto> CreateEquipmentSchema(CreateEquipmentSchemaDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(dto.EquipmentTypeId, cancellationToken);

        var equipmentSchema = new EquipmentSchema
        {
            Name = dto.Name,
            ParameterValues = dto.ParameterValues,
            EquipmentTypeId = equipmentType.Id
        };

        context.Add(equipmentSchema);

        await context.Commit(cancellationToken);

        equipmentSchema.EquipmentType = equipmentType.Map(EquipmentTypeDtoConverter.Convert);
        return EquipmentSchemaDtoConverter.Convert(equipmentSchema);
    }

    public async Task<EquipmentSchemaDto> PatchEquipmentSchema(
        int equipmentSchemaId,
        PatchEquipmentSchemaDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentSchemaToPatch = await GetEquipmentSchemaByIdInner(equipmentSchemaId, cancellationToken, context);

        equipmentSchemaToPatch.Update(
            dto.Name,
            null!,
            dto.ParameterValues,
            []);

        await context.Commit(cancellationToken);

        return EquipmentSchemaDtoConverter.Convert(equipmentSchemaToPatch);
    }

    private async Task<EquipmentSchema> GetEquipmentSchemaByIdInner(
        int equipmentSchemaId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = equipmentSchemaQueryFactory.FindById(equipmentSchemaId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentSchemaNotFoundException($"EquipmentSchema [{equipmentSchemaId}] not found");
    }
}