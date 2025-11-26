using Commons;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Requests;
using Rooms.Core.Dtos.Room.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Core.Services.Files.Mappers;
using Rooms.Core.Services.Rooms.Interfaces;
using Rooms.Core.Services.Rooms.Mappers;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Rooms;
using Rooms.Domain.Models.Rooms.Fix;
using Rooms.Domain.Models.Rooms.Parameters;

namespace Rooms.Core.Services.Rooms;

public class RoomService(IUnitOfWorkFactory unitOfWorkFactory) : IRoomService
{
    public async Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var room = await GetRoomByIdInner(unitOfWork, roomId, cancellationToken);

        return room.Map(RoomDtoMapper.Map);
    }

    public async Task<RoomDto[]> FindRoomByIds(int[] roomIds, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var request = new FindRoomsByIdQuery(roomIds);
        var rooms = await unitOfWork.ApplyQuery(request, cancellationToken);

        return rooms.ToBlockingEnumerable(cancellationToken: cancellationToken).Select(RoomDtoMapper.Map).ToArray();
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterRoomsQuery(requestDto.BatchSize, requestDto.BatchNumber, requestDto.AfterRoomId, requestDto.Filter);

        var rooms = await (await unitOfWork.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedRooms = rooms.Select(RoomDtoMapper.Map).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsResponseDto(convertedRooms, convertedRooms.Length, lastRoomId);
    }

    public async Task<IEnumerable<AutocompleteRoomResponseDto>> AutocompleteRoom(string roomName, CancellationToken cancellationToken)
    {
        // todo
        return [new AutocompleteRoomResponseDto { RoomId = 1, ViewRoomName = Guid.NewGuid().ToString() }];
    }

    public async Task<RoomDto> CreateRoom(CreateRoomDto dto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        await Validate(unitOfWork, dto, cancellationToken);

        var room = new Room(
            dto.Name,
            dto.Description,
            new RoomParameters
            {
                Type = RoomDtoMapper.Map(dto.Type),
                Layout = RoomDtoMapper.Map(dto.Layout),
                NetType = RoomDtoMapper.Map(dto.NetType),
                Seats = dto.Seats,
                ComputerSeats = dto.ComputerSeats,
                HasConditioning = dto.HasConditioning
            },
            new RoomAttachments
            {
                PdfRoomScheme = FileDtoMapper.Convert(dto.PdfRoomSchemeFile),
                Photo = FileDtoMapper.Convert(dto.PhotoFile)
            },
            dto.Owner,
            new RoomFixInfo
            {
                Status = RoomDtoMapper.Map(dto.RoomStatus),
                FixDeadline = dto.FixDeadline,
                Comment = dto.Comment
            },
            dto.AllowBooking);

        unitOfWork.Add(room);

        await unitOfWork.Commit(cancellationToken);

        return RoomDtoMapper.Map(room);
    }

    public async Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var roomToPatch = await GetRoomByIdInner(unitOfWork, roomId, cancellationToken);

        roomToPatch.Update(
            dto.Name,
            dto.Description,
            new RoomParameters
            {
                Type = RoomDtoMapper.Map(dto.Type),
                Layout = RoomDtoMapper.Map(dto.Layout),
                NetType = RoomDtoMapper.Map(dto.NetType),
                Seats = dto.Seats,
                ComputerSeats = dto.ComputerSeats,
                HasConditioning = dto.HasConditioning
            },
            new RoomAttachments
            {
                PdfRoomScheme = FileDtoMapper.Convert(dto.PdfRoomSchemeFile),
                Photo = FileDtoMapper.Convert(dto.PhotoFile)
            },
            dto.Owner,
            new RoomFixInfo
            {
                Status = RoomDtoMapper.Map(dto.RoomStatus),
                FixDeadline = dto.FixDeadline,
                Comment = dto.Comment
            },
            dto.AllowBooking);

        if (dto.ScheduleAddress is not null)
        {
            roomToPatch.SetScheduleAddress(
                dto.ScheduleAddress.RoomNumber,
                dto.ScheduleAddress.Address);
        }

        await unitOfWork.Commit(cancellationToken);

        return RoomDtoMapper.Map(roomToPatch);
    }

    private async Task<Room> GetRoomByIdInner(IUnitOfWork unitOfWork, int roomId, CancellationToken cancellationToken)
    {
        var query = new FindRoomByIdQuery(roomId);

        return await unitOfWork.ApplyQuery(query, cancellationToken)
               ?? throw new RoomNotFoundException($"Room [{roomId}] not found");
    }

    private async Task Validate(IUnitOfWork unitOfWork, CreateRoomDto dto, CancellationToken cancellationToken)
    {
        var query = new FindRoomByNameQuery(dto.Name);
        var room = await unitOfWork.ApplyQuery(query, cancellationToken);

        if (room is not null)
        {
            throw new RoomAlreadyCreatedException($"Room with name [{dto.Name}] already exists");
        }
    }
}