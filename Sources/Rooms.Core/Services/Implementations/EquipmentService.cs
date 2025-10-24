using Commons;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.EquipmentModels;
using Rooms.Domain.Queries;
using Rooms.Persistence;
using Rooms.Persistence.Queries.Equipment;
using EquipmentDtoConverter = Rooms.Core.DtoConverters.EquipmentDtoConverter;
using RoomDtoConverter = Rooms.Core.DtoConverters.RoomDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class EquipmentService(IDbContextFactory<RoomsDbContext> domainDbContextProvider) : IEquipmentService
{
    public async Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipment = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        return equipment.Map(EquipmentDtoConverter.Convert);
    }

    public async Task<EquipmentsResponseDto> FilterEquipments(GetEquipmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipments = await context
            .ApplyQuery(new FilterEquipmentsQuery
            {
                BatchSize = dto.BatchSize,
                BatchNumber = dto.BatchNumber,
                AfterEquipmentId = dto.AfterEquipmentId,
                Filter = dto.Filter.AsOptional().Map(FiltersDtoConverter.Convert),
            })
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedEquipments = equipments.Select(EquipmentDtoConverter.Convert).ToArray();
        int? lastEquipmentId = convertedEquipments.Length == 0 ? null : convertedEquipments.Select(t => t.Id).Max();

        return new EquipmentsResponseDto(convertedEquipments, convertedEquipments.Length, lastEquipmentId);
    }

    public async Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipmentToCreate = Equipment.New(
            dto.Room.Map(RoomDtoConverter.Convert),
            dto.SchemaDto.Map(EquipmentDtoConverter.Convert),
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        var equipmentEntity = context.Equipments.Add(equipmentToCreate);

        await context.SaveChangesAsync(cancellationToken);

        return EquipmentDtoConverter.Convert(equipmentEntity.Entity);
    }

    public async Task<EquipmentDto> PatchEquipment(
        int equipmentId,
        PatchEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipmentToPatch = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        equipmentToPatch.Update(
            dto.Room.Map(RoomDtoConverter.Convert),
            dto.SchemaDto.Map(EquipmentDtoConverter.Convert),
            dto.InventoryNumber,
            dto.SerialNumber,
            dto.NetworkEquipmentIp,
            dto.Comment,
            dto.Status);

        await context.SaveChangesAsync(cancellationToken);

        return EquipmentDtoConverter.Convert(equipmentToPatch);
    }

    private static async Task<Equipment> GetEquipmentByIdInner(
        int equipmentId,
        CancellationToken cancellationToken,
        RoomsDbContext context)
    {
        return await context.ApplyQuery(new FindEquipmentByIdQuery { EquipmentId = equipmentId }, cancellationToken)
               ?? throw new EquipmentNotFoundException($"Equipment [{equipmentId}] not found");
    }
}