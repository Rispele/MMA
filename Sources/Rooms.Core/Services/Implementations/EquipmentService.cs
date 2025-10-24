using Commons;
using Commons.Optional;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipment;
using Rooms.Domain.Queries.Abstractions;
using Rooms.Domain.Queries.Factories;
using EquipmentDtoConverter = Rooms.Core.DtoConverters.EquipmentDtoConverter;
using RoomDtoConverter = Rooms.Core.DtoConverters.RoomDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class EquipmentService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEquipmentQueryFactory equipmentQueryFactory) : IEquipmentService
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

        var domainFilter = dto.Filter.AsOptional().Map(FiltersDtoConverter.Convert);
        var query = equipmentQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentId, domainFilter);
        
        var equipments = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedEquipments = equipments.Select(EquipmentDtoConverter.Convert).ToArray();
        int? lastEquipmentId = convertedEquipments.Length == 0 ? null : convertedEquipments.Select(t => t.Id).Max();

        return new EquipmentsResponseDto(convertedEquipments, convertedEquipments.Length, lastEquipmentId);
    }

    public async Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipment = Equipment.New(
            dto.Room.Map(RoomDtoConverter.Convert),
            dto.SchemaDto.Map(EquipmentDtoConverter.Convert),
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        context.Add(equipment);

        await context.Commit(cancellationToken);

        return EquipmentDtoConverter.Convert(equipment);
    }

    public async Task<EquipmentDto> PatchEquipment(
        int equipmentId,
        PatchEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentToPatch = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        equipmentToPatch.Update(
            dto.Room.Map(RoomDtoConverter.Convert),
            dto.SchemaDto.Map(EquipmentDtoConverter.Convert),
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