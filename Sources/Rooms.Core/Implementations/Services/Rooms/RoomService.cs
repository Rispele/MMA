using Commons;
using Commons.Queries;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Implementations.Dtos.Requests.RoomCreating;
using Rooms.Core.Implementations.Dtos.Requests.RoomPatching;
using Rooms.Core.Implementations.Dtos.Requests.RoomsQuerying;
using Rooms.Core.Implementations.Dtos.Responses;
using Rooms.Core.Implementations.Dtos.Room;
using Rooms.Core.Implementations.Persistence;
using Rooms.Core.Implementations.Services.DtoConverters;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;

namespace Rooms.Core.Implementations.Services.Rooms;

public class RoomService(
    IDbContextFactory<RoomsDbContext> domainDbContextProvider,
    RoomDtoConverter roomDtoConverter,
    FileDtoConverter fileDtoConverter)
    : IRoomService
{
    public async Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var room = await GetRoomByIdInner(roomId, cancellationToken, context);

        return room.Map(roomDtoConverter.Convert);
    }

    public async Task<RoomsBatchDto> FilterRooms(GetRoomsRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var rooms = await context
            .ApplyQuery(new FilterRoomsQuery(request.BatchSize, request.BatchNumber, request.AfterRoomId, request.Filter, roomDtoConverter))
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedRooms = rooms.Select(roomDtoConverter.Convert).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsBatchDto(convertedRooms, convertedRooms.Length, lastRoomId);
    }

    public async Task<RoomDto> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        await Validate(context, request, cancellationToken);

        var roomToCreate = Room.New(
            request.Name,
            request.Description,
            new RoomParameters(
                roomDtoConverter.Convert(request.Type),
                roomDtoConverter.Convert(request.Layout),
                roomDtoConverter.Convert(request.NetType),
                request.Seats,
                request.ComputerSeats,
                request.HasConditioning),
            new RoomAttachments(
                fileDtoConverter.Convert(request.PdfRoomSchemeFileMetadata),
                fileDtoConverter.Convert(request.PhotoFileMetadata)),
            request.Owner,
            new RoomFixInfo(
                roomDtoConverter.Convert(request.RoomStatus),
                request.FixDeadline,
                request.Comment),
            request.AllowBooking);

        var roomEntity = context.Rooms.Add(roomToCreate);

        await context.SaveChangesAsync(cancellationToken);

        return roomDtoConverter.Convert(roomEntity.Entity);
    }

    public async Task<RoomDto> PatchRoom(int roomId, PatchRoomRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var roomToPatch = await GetRoomByIdInner(roomId, cancellationToken, context);

        roomToPatch.Update(
            request.Name,
            request.Description,
            new RoomParameters(
                roomDtoConverter.Convert(request.Type),
                roomDtoConverter.Convert(request.Layout),
                roomDtoConverter.Convert(request.NetType),
                request.Seats,
                request.ComputerSeats,
                request.HasConditioning),
            new RoomAttachments(
                fileDtoConverter.Convert(request.PdfRoomSchemeFileMetadata),
                fileDtoConverter.Convert(request.PhotoFileMetadata)),
            request.Owner,
            new RoomFixInfo(
                roomDtoConverter.Convert(request.RoomStatus),
                request.FixDeadline,
                request.Comment),
            request.AllowBooking);

        await context.SaveChangesAsync(cancellationToken);

        return roomDtoConverter.Convert(roomToPatch);
    }

    private static async Task<Room> GetRoomByIdInner(int roomId, CancellationToken cancellationToken, RoomsDbContext context)
    {
        return await context.ApplyQuery(new FindRoomByIdQuery(roomId), cancellationToken)
               ?? throw new RoomNotFoundException($"Room [{roomId}] not found");
    }

    private async Task Validate(RoomsDbContext dbContext, CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var room = await dbContext.ApplyQuery(new FindRoomByNameQuery(request.Name), cancellationToken);

        if (room is not null)
        {
            throw new RoomAlreadyCreatedException($"Room with name [{request.Name}] already exists");
        }
    }
}