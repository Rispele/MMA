using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;
using Application.Implementations.Persistence;
using Application.Implementations.Services.DtoConverters;
using Commons;
using Commons.Queries;
using Domain.Exceptions;
using Domain.Models.Room;
using Domain.Models.Room.Fix;
using Domain.Models.Room.Parameters;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations.Services.Rooms;

public class RoomService(IDbContextFactory<DomainDbContext> domainDbContextProvider,
    RoomDtoConverter roomDtoConverter,
    FileDtoConverter fileDtoConverter)
    : IRoomService
{
    public async Task<RoomDto> GetRoomById(int id, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var room = await context.ApplyQuery(new FindRoomByIdQuery(id), cancellationToken)
                   ?? throw new RoomNotFoundException($"Room [{id}] not found");

        return room.Map(roomDtoConverter.Convert);
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var rooms = await context
            .ApplyQuery(new FilterRoomsQuery(request.BatchSize, request.BatchNumber, request.AfterRoomId, request.Filter, roomDtoConverter))
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedRooms = rooms.Select(roomDtoConverter.Convert).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsResponseDto(convertedRooms, convertedRooms.Length, lastRoomId);
    }

    public async Task<RoomDto> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken)
    {
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

        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var roomEntity = context.Rooms.Add(roomToCreate);

        await context.SaveChangesAsync(cancellationToken);

        return roomDtoConverter.Convert(roomEntity.Entity);
    }

    public async Task<RoomDto> PatchRoom(int roomId, PatchRoomRequest request, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var roomToPatch = await context.Rooms.FindAsync([roomId], cancellationToken: cancellationToken);

        if (roomToPatch is null)
        {
            throw new RoomNotFoundException($"Room [{roomId}] not found");
        }

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
}