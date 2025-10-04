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
using Domain.Persistence;

namespace Application.Implementations.Services.Rooms;

public class RoomService : IRoomService
{
    private readonly DomainDbContext dbContext;
    private readonly RoomDtoConverter roomDtoConverter;

    public RoomService(DomainDbContext dbContext, RoomDtoConverter roomDtoConverter)
    {
        this.dbContext = dbContext;
        this.roomDtoConverter = roomDtoConverter;
    }

    public async Task<RoomDto> GetRoomById(int id, CancellationToken cancellationToken)
    {
        var room = await dbContext.ApplyQuery(new FindRoomByIdQuery(id), cancellationToken)
            ?? throw new RoomNotFoundException($"Room [{id}] not found");

        return room.Map(roomDtoConverter.Convert);
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto request, CancellationToken cancellationToken)
    {
        var rooms = await dbContext
            .ApplyQuery(new FilterRoomsQuery(request.BatchSize, request.BatchNumber, request.AfterRoomId, request.Filter))
            .ToArrayAsync(cancellationToken: cancellationToken);

        var convertedRooms = rooms.Select(roomDtoConverter.Convert).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsResponseDto(convertedRooms, convertedRooms.Length, lastRoomId);
    }

    public Task<RoomDto> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> PatchRoom(int roomId, PatchRoomRequestDto patchedDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}