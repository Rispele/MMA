using Commons;
using Rooms.Core.Clients;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.OperatorRoom;
using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.OperatorRoom;

namespace Rooms.Core.Services.Implementations;

public class OperatorRoomService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IOperatorRoomQueryFactory operatorRoomQueryFactory,
    IRoomService roomService,
    IOperatorRoomClient operatorRoomClient) : IOperatorRoomService
{
    public async Task<OperatorRoomDto> GetOperatorRoomById(int operatorRoomId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorRoom = await GetOperatorRoomByIdInner(operatorRoomId, cancellationToken, context);

        return operatorRoom.Map(OperatorRoomDtoConverter.Convert);
    }

    public async Task<Dictionary<Guid, string>> GetAvailableOperators(CancellationToken cancellationToken)
    {
        var operators = await operatorRoomClient.GetAvailableOperators();

        return operators;
    }

    public async Task<OperatorRoomsResponseDto> FilterOperatorRooms(GetOperatorRoomsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = operatorRoomQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterOperatorRoomId, dto.Filter);

        var operatorRooms = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken);

        var convertedOperatorRooms = operatorRooms.Select(OperatorRoomDtoConverter.Convert).ToArray();
        int? lastOperatorRoomId = convertedOperatorRooms.Length == 0 ? null : convertedOperatorRooms.Select(t => t.Id).Max();

        return new OperatorRoomsResponseDto(convertedOperatorRooms, convertedOperatorRooms.Length, lastOperatorRoomId);
    }

    public async Task<OperatorRoomDto> CreateOperatorRoom(CreateOperatorRoomDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var rooms = (await roomService.GetRoomsById(dto.RoomIds, cancellationToken)).ToArray();

        var operatorRoom = new OperatorRoom
        {
            Name = dto.Name,
            Operators = dto.Operators,
            Contacts = dto.Contacts,
        };

        context.Add(operatorRoom);

        await context.Commit(cancellationToken);

        operatorRoom.Rooms = [];
        foreach (var roomId in dto.RoomIds)
        {
            var roomToPatch = rooms.First(x => x.Id == roomId);
            roomToPatch.OperatorRoomId = operatorRoom.Id;
            var patchedRoom = await roomService.PatchRoom(roomId, roomToPatch.Map(RoomDtoConverter.ConvertPatch),
                cancellationToken);
            operatorRoom.Rooms.Add(patchedRoom.Map(RoomDtoConverter.Convert));
        }

        return OperatorRoomDtoConverter.Convert(operatorRoom);
    }

    public async Task<OperatorRoomDto> PatchOperatorRoom(
        int operatorRoomId,
        PatchOperatorRoomDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorRoomToPatch = await GetOperatorRoomByIdInner(operatorRoomId, cancellationToken, context);

        operatorRoomToPatch.Update(
            dto.Name,
            dto.Operators,
            dto.Contacts);

        await context.Commit(cancellationToken);

        return OperatorRoomDtoConverter.Convert(operatorRoomToPatch);
    }

    private async Task<OperatorRoom> GetOperatorRoomByIdInner(
        int operatorRoomId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = operatorRoomQueryFactory.FindById(operatorRoomId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new OperatorRoomNotFoundException($"OperatorRoom [{operatorRoomId}] not found");
    }
}
