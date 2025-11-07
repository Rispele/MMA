using Commons;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.ExcelExporters.Exporters;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Implementations;

public class EquipmentTypeService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEquipmentTypeQueryFactory equipmentTypeQueryFactory,
    IRoomService roomService) : IEquipmentTypeService
{
    public async Task<EquipmentTypeDto> GetEquipmentTypeById(int equipmentTypeId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentType);
    }

    public async Task<EquipmentTypesResponseDto> FilterEquipmentTypes(GetEquipmentTypesDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = equipmentTypeQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentTypeId, dto.Filter);

        var equipmentTypes = await context
            .ApplyQuery(query)
            .ToListAsync(cancellationToken);

        var convertedEquipmentTypes = equipmentTypes.Select(EquipmentTypeDtoMapper.MapEquipmentTypeToDto).ToArray();
        int? lastEquipmentTypeId = convertedEquipmentTypes.Length == 0 ? null : convertedEquipmentTypes.Select(t => t.Id).Max();

        return new EquipmentTypesResponseDto(convertedEquipmentTypes, convertedEquipmentTypes.Length, lastEquipmentTypeId);
    }

    public async Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = new EquipmentType
        {
            Name = dto.Name,
            Parameters = dto.Parameters.Select(x => new EquipmentParameterDescriptor
            {
                Name = x.Name,
                Required = x.Required,
            }).ToList()
        };

        context.Add(equipmentType);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentType);
    }

    public async Task<EquipmentTypeDto> PatchEquipmentType(
        int equipmentTypeId,
        PatchEquipmentTypeDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentTypeToPatch = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        var updatedParameters = dto.Parameters
            .Select(x => new EquipmentParameterDescriptor
            {
                Name = x.Name,
                Required = x.Required,
            })
            .ToList();

        equipmentTypeToPatch.Update(dto.Name, updatedParameters);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentTypeToPatch);
    }

    public async Task<FileExportDto> ExportEquipmentTypeRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentTypeRegistryExcelExportDto
            {
                Name = string.Empty,
            }
        };
        var exporter = new EquipmentTypeRegistryExcelExporter();
        return exporter.Export(exportDtos, cancellationToken);
    }

    private async Task<EquipmentType> GetEquipmentTypeByIdInner(
        int equipmentTypeId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = equipmentTypeQueryFactory.FindById(equipmentTypeId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentTypeNotFoundException($"EquipmentType [{equipmentTypeId}] not found");
    }
}