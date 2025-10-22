using Commons;
using Commons.Queries;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Implementations.Dtos.Equipment;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentCreating;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentPatching;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;
using Rooms.Core.Implementations.Dtos.Responses;
using Rooms.Core.Implementations.Persistence;
using Rooms.Core.Implementations.Services.DtoConverters;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipment;
using Rooms.Domain.Persistence;

namespace Rooms.Core.Implementations.Services.Equipments;

public class EquipmentService(IDbContextFactory<RoomsDbContext> domainDbContextProvider): IEquipmentService
{
    public async Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipment = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        return equipment.Map(EquipmentDtoConverter.Convert);
    }

    public async Task<EquipmentsBatchDto> FilterEquipments(GetEquipmentsRequestDto request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipments = await context
            .ApplyQuery(new FilterEquipmentsQuery(request.BatchSize, request.BatchNumber, request.AfterEquipmentId, request.Filter))
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedEquipments = equipments.Select(EquipmentDtoConverter.Convert).ToArray();
        int? lastEquipmentId = convertedEquipments.Length == 0 ? null : convertedEquipments.Select(t => t.Id).Max();

        return new EquipmentsBatchDto(convertedEquipments, convertedEquipments.Length, lastEquipmentId);
    }

    public async Task<EquipmentDto> CreateEquipment(CreateEquipmentRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipmentToCreate = Equipment.New(
            request.Room.Map(RoomDtoConverter.Convert),
            request.SchemaDto.Map(EquipmentDtoConverter.Convert),
            request.InventoryNumber,
            request.SerialNumber,
            request.NetworkEquipmentIp,
            request.Comment,
            request.Status);

        var equipmentEntity = context.Equipments.Add(equipmentToCreate);

        await context.SaveChangesAsync(cancellationToken);

        return EquipmentDtoConverter.Convert(equipmentEntity.Entity);
    }

    public async Task<EquipmentDto> PatchEquipment(int equipmentId, PatchEquipmentRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var equipmentToPatch = await GetEquipmentByIdInner(equipmentId, cancellationToken, context);

        equipmentToPatch.Update(
            request.Room.Map(RoomDtoConverter.Convert),
            request.SchemaDto.Map(EquipmentDtoConverter.Convert),
            request.InventoryNumber,
            request.SerialNumber,
            request.NetworkEquipmentIp,
            request.Comment,
            request.Status);

        await context.SaveChangesAsync(cancellationToken);

        return EquipmentDtoConverter.Convert(equipmentToPatch);
    }

    private static async Task<Equipment> GetEquipmentByIdInner(int equipmentId, CancellationToken cancellationToken, RoomsDbContext context)
    {
        return await context.ApplyQuery(new FindEquipmentByIdQuery(equipmentId), cancellationToken)
               ?? throw new EquipmentNotFoundException($"Equipment [{equipmentId}] not found");
    }
}