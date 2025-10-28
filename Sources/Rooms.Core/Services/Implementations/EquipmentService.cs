using Commons;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipment;
using EquipmentDtoConverter = Rooms.Core.DtoConverters.EquipmentDtoConverter;
using RoomDtoConverter = Rooms.Core.DtoConverters.RoomDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class EquipmentService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEquipmentQueryFactory equipmentQueryFactory,
    IRoomService roomService,
    IEquipmentSchemaService equipmentSchemaService) : IEquipmentService
{
    public async Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipment = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        return equipment.Map(EquipmentDtoConverter.Convert);
    }

    public async Task<EquipmentsResponseDto> FilterEquipments(GetEquipmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = equipmentQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentId, dto.Filter);

        var equipments = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken);

        var convertedEquipments = equipments.Select(EquipmentDtoConverter.Convert).ToArray();
        int? lastEquipmentId = convertedEquipments.Length == 0 ? null : convertedEquipments.Select(t => t.Id).Max();

        return new EquipmentsResponseDto(convertedEquipments, convertedEquipments.Length, lastEquipmentId);
    }

    public async Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var room = await roomService.GetRoomById(dto.RoomId, cancellationToken);
        var schema = await equipmentSchemaService.GetEquipmentSchemaById(dto.SchemaId, cancellationToken);

        var equipment = new Equipment
        {
            RoomId = room.Id,
            SchemaId = schema.Id,
            InventoryNumber = dto.InventoryNumber,
            SerialNumber = dto.SerialNumber,
            NetworkEquipmentIp = dto.NetworkEquipmentIp,
            Comment = dto.Comment,
            Status = dto.Status,
        };

        context.Add(equipment);

        await context.Commit(cancellationToken);

        equipment.Room = room.Map(RoomDtoConverter.Convert);
        equipment.Schema = schema.Map(EquipmentSchemaDtoConverter.Convert);
        return equipment.Map(EquipmentDtoConverter.Convert);
    }

    public async Task<EquipmentDto> PatchEquipment(
        int equipmentId,
        PatchEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentToPatch = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        equipmentToPatch.Update(
            (await roomService.GetRoomById(dto.RoomId, cancellationToken)).Map(RoomDtoConverter.Convert),
            dto.SchemaDto.Map(EquipmentSchemaDtoConverter.Convert),
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        await context.Commit(cancellationToken);

        return EquipmentDtoConverter.Convert(equipmentToPatch);
    }

    private async Task<Equipment> GetEquipmentByIdInner(
        int equipmentId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = equipmentQueryFactory.FindById(equipmentId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentNotFoundException($"Equipment [{equipmentId}] not found");
    }
}