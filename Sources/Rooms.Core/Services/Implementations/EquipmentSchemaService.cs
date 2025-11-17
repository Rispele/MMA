using Commons;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.ExcelExporters.Exporters;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Implementations;

public class EquipmentSchemaService(IUnitOfWorkFactory unitOfWorkFactory) : IEquipmentSchemaService
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

        var equipmentSchema = new EquipmentSchema
        {
            Name = dto.Name,
            ParameterValues = dto.ParameterValues,
            Type = equipmentType,
        };

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

    public async Task<FileExportDto> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentSchemaRegistryExcelExportDto
            {
                EquipmentName = string.Empty,
                EquipmentType = string.Empty,
                Parameters = string.Empty,
            }
        };
        var exporter = new EquipmentSchemaRegistryExcelExporter();
        return exporter.Export(exportDtos, cancellationToken);
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