using Commons;
using Commons.Queries;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using Rooms.Core.PersistenceModels;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using Rooms.Domain.Persistence;
using FileDtoConverter = Rooms.Core.DtoConverters.FileDtoConverter;
using RoomDtoConverter = Rooms.Core.DtoConverters.RoomDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class RoomService(IDbContextFactory<RoomsDbContext> domainDbContextProvider) : IRoomService
{
    public async Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var room = await GetRoomByIdInner(roomId, cancellationToken, context);

        return room.Map(RoomDtoConverter.Convert);
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var rooms = await context
            .ApplyQuery(new FilterRoomsQuery(dto.BatchSize, dto.BatchNumber, dto.AfterRoomId, dto.Filter))
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedRooms = rooms.Select(RoomDtoConverter.Convert).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsResponseDto(convertedRooms, convertedRooms.Length, lastRoomId);
    }

    public async Task<RoomDto> CreateRoom(CreateRoomDto dto, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        await Validate(context, dto, cancellationToken);

        var roomToCreate = Room.New(
            dto.Name,
            dto.Description,
            new RoomParameters(
                RoomDtoConverter.Convert(dto.Type),
                RoomDtoConverter.Convert(dto.Layout),
                RoomDtoConverter.Convert(dto.NetType),
                dto.Seats,
                dto.ComputerSeats,
                dto.HasConditioning),
            new RoomAttachments(
                FileDtoConverter.Convert(dto.PdfRoomSchemeFileMetadata),
                FileDtoConverter.Convert(dto.PhotoFileMetadata)),
            dto.Owner,
            new RoomFixInfo(
                RoomDtoConverter.Convert(dto.RoomStatus),
                dto.FixDeadline,
                dto.Comment),
            dto.AllowBooking);

        var roomEntity = context.Rooms.Add(roomToCreate);

        await context.SaveChangesAsync(cancellationToken);

        return RoomDtoConverter.Convert(roomEntity.Entity);
    }

    public async Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken)
    {
        await using var context = await domainDbContextProvider.CreateDbContextAsync(cancellationToken);

        var roomToPatch = await GetRoomByIdInner(roomId, cancellationToken, context);

        roomToPatch.Update(
            dto.Name,
            dto.Description,
            new RoomParameters(
                RoomDtoConverter.Convert(dto.Type),
                RoomDtoConverter.Convert(dto.Layout),
                RoomDtoConverter.Convert(dto.NetType),
                dto.Seats,
                dto.ComputerSeats,
                dto.HasConditioning),
            new RoomAttachments(
                FileDtoConverter.Convert(dto.PdfRoomSchemeFileMetadata),
                FileDtoConverter.Convert(dto.PhotoFileMetadata)),
            dto.Owner,
            new RoomFixInfo(
                RoomDtoConverter.Convert(dto.RoomStatus),
                dto.FixDeadline,
                dto.Comment),
            dto.AllowBooking);

        await context.SaveChangesAsync(cancellationToken);

        return RoomDtoConverter.Convert(roomToPatch);
    }

    private static async Task<Room> GetRoomByIdInner(int roomId, CancellationToken cancellationToken, RoomsDbContext context)
    {
        return await context.ApplyQuery(new FindRoomByIdQuery(roomId), cancellationToken)
               ?? throw new RoomNotFoundException($"Room [{roomId}] not found");
    }

    private async Task Validate(RoomsDbContext dbContext, CreateRoomDto dto, CancellationToken cancellationToken)
    {
        var room = await dbContext.ApplyQuery(new FindRoomByNameQuery(dto.Name), cancellationToken);

        if (room is not null)
        {
            throw new RoomAlreadyCreatedException($"Room with name [{dto.Name}] already exists");
        }
    }
}