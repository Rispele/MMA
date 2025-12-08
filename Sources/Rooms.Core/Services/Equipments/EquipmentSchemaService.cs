using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Core.Services.Equipments.Mappers;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Propagated.Exceptions;

namespace Rooms.Core.Services.Equipments;

internal class EquipmentSchemaService([RoomsScope] IUnitOfWorkFactory unitOfWorkFactory) : IEquipmentSchemaService
{
    public async Task<EquipmentSchemaDto> GetEquipmentSchemaById(int equipmentSchemaId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentSchema = await GetEquipmentSchemaByIdInner(equipmentSchemaId, cancellationToken, context);

        return EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(equipmentSchema);
    }

    public async Task<EquipmentSchemasResponseDto> FilterEquipmentSchemas(GetEquipmentSchemasDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterEquipmentSchemasQuery(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentSchemaId, dto.Filter);

        var equipmentSchemas = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedEquipmentSchemas = equipmentSchemas.Select(EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto).ToArray();
        int? lastEquipmentSchemaId = convertedEquipmentSchemas.Length == 0 ? null : convertedEquipmentSchemas.Select(t => t.Id).Max();

        return new EquipmentSchemasResponseDto(convertedEquipmentSchemas, convertedEquipmentSchemas.Length, lastEquipmentSchemaId);
    }

    public async Task<EquipmentSchemaDto> CreateEquipmentSchema(CreateEquipmentSchemaDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = await context.ApplyQuery(new FindEquipmentTypeByIdQuery(dto.EquipmentTypeId), cancellationToken);

        var equipmentSchema = new EquipmentSchema(dto.Name, equipmentType, dto.ParameterValues);

        context.Add(equipmentSchema);

        await context.Commit(cancellationToken);

        return EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(equipmentSchema);
    }

    public async Task<EquipmentSchemaDto> PatchEquipmentSchema(
        int equipmentSchemaId,
        PatchEquipmentSchemaDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentSchemaToPatch = await GetEquipmentSchemaByIdInner(equipmentSchemaId, cancellationToken, context);
        var equipmentType = await context.ApplyQuery(new FindEquipmentTypeByIdQuery(dto.EquipmentTypeId), cancellationToken);

        equipmentSchemaToPatch.Update(
            dto.Name,
            equipmentType,
            dto.ParameterValues);

        await context.Commit(cancellationToken);

        return EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(equipmentSchemaToPatch);
    }

    private async Task<EquipmentSchema> GetEquipmentSchemaByIdInner(
        int equipmentSchemaId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindEquipmentSchemaByIdQuery(equipmentSchemaId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentSchemaNotFoundException($"EquipmentSchema [{equipmentSchemaId}] not found");
    }
}