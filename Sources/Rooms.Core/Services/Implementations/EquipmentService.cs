using Commons;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.ExcelExporters.Exporters;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Services.Implementations;

public class EquipmentService(IUnitOfWorkFactory unitOfWorkFactory, IRoomService roomService) : IEquipmentService
{
    public async Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipment = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        return EquipmentDtoMapper.MapEquipmentToDto(equipment);
    }

    public async Task<EquipmentsResponseDto> FilterEquipments(GetEquipmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterEquipmentsQuery(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentId, dto.Filter);

        var equipments = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedEquipments = equipments.Select(EquipmentDtoMapper.MapEquipmentToDto).ToArray();
        int? lastEquipmentId = convertedEquipments.Length == 0 ? null : convertedEquipments.Select(t => t.Id).Max();

        return new EquipmentsResponseDto(convertedEquipments, convertedEquipments.Length, lastEquipmentId);
    }

    public async Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var room = await roomService.GetRoomById(dto.RoomId, cancellationToken);
        var equipmentSchema = await context.ApplyQuery(new FindEquipmentSchemaByIdQuery(dto.SchemaId), cancellationToken);

        var equipment = new Equipment
        {
            RoomId = room.Id,
            Schema = equipmentSchema,
            InventoryNumber = dto.InventoryNumber,
            SerialNumber = dto.SerialNumber,
            NetworkEquipmentIp = dto.NetworkEquipmentIp,
            Comment = dto.Comment,
            Status = dto.Status
        };

        context.Add(equipment);

        await context.Commit(cancellationToken);

        return EquipmentDtoMapper.MapEquipmentToDto(equipment);
    }

    public async Task<EquipmentDto> PatchEquipment(
        int equipmentId,
        PatchEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentToPatch = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);
        var equipmentSchema = await context.ApplyQuery(new FindEquipmentSchemaByIdQuery(dto.SchemaId), cancellationToken);

        equipmentToPatch.Update(
            equipmentToPatch.RoomId,
            equipmentSchema,
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        await context.Commit(cancellationToken);

        return EquipmentDtoMapper.MapEquipmentToDto(equipmentToPatch);
    }

    public async Task<FileExportDto> ExportEquipmentRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new EquipmentRegistryExcelExportDto
            {
                RoomName = string.Empty,
                EquipmentType = string.Empty,
                EquipmentSchemaName = string.Empty,
            }
        };
        var exporter = new EquipmentRegistryExcelExporter();
        return exporter.Export(exportDtos, cancellationToken);
    }

    private async Task<Equipment> GetEquipmentByIdInner(
        int equipmentId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindEquipmentByIdQuery(equipmentId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentNotFoundException($"Equipment [{equipmentId}] not found");
    }
}