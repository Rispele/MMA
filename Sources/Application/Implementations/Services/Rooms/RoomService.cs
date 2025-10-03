using Application.Implementations.Dtos.Requests.RoomCreating;
using Application.Implementations.Dtos.Requests.RoomPatching;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Dtos.Responses;
using Application.Implementations.Dtos.Room;
using Application.Implementations.Services.DtoConverters;
using Commons;
using Domain.Exceptions;
using Domain.Persistence;
using Domain.Persistence.Queries;

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
        var room = await dbContext.ApplyQuery(new FindRoomByIdQuery(id))
            ?? throw new RoomNotFoundException($"Room [{id}] not found");

        return room.Map(roomDtoConverter.Convert);
    }

    public Task<RoomsResponseDto> GetRooms(RoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> CreateRoom(PostRoomRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> PatchRoom(int roomId, PatchRoomRequestDto patchedDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}