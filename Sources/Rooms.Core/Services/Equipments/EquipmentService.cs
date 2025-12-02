using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Core.Services.Equipments.Mappers;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Propagated.Exceptions;

namespace Rooms.Core.Services.Equipments;

internal class EquipmentService([RoomsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IRoomService roomService,
    IEquipmentSchemaService equipmentSchemaService) : IEquipmentService
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
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(dto.SchemaId, cancellationToken);

        var equipment = new Equipment(
            room.Id,
            equipmentSchema.Id,
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

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
        var room = await roomService.GetRoomById(dto.RoomId, cancellationToken);
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(dto.SchemaId, cancellationToken);

        equipmentToPatch.Update(
            room.Id,
            equipmentSchema.Id,
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        await context.Commit(cancellationToken);

        return EquipmentDtoMapper.MapEquipmentToDto(equipmentToPatch);
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