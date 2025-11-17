using Commons;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using FileDtoConverter = Rooms.Core.DtoConverters.FileDtoConverter;
using RoomDtoConverter = Rooms.Core.DtoConverters.RoomDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class RoomService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IRoomQueriesFactory queriesFactory) : IRoomService
{
    public async Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var room = await GetRoomByIdInner(unitOfWork, roomId, cancellationToken);

        return room.Map(RoomDtoConverter.Convert);
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var query = queriesFactory.Filter(requestDto.BatchSize, requestDto.BatchNumber, requestDto.AfterRoomId, requestDto.Filter);

        var rooms = await unitOfWork
            .ApplyQuery(query)
            .ToListAsync(cancellationToken);

        var convertedRooms = rooms.Select(RoomDtoConverter.Convert).ToArray();
        int? lastRoomId = convertedRooms.Length == 0 ? null : convertedRooms.Select(t => t.Id).Max();

        return new RoomsResponseDto(convertedRooms, convertedRooms.Length, lastRoomId);
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
                Type = RoomDtoConverter.Convert(dto.Type),
                Layout = RoomDtoConverter.Convert(dto.Layout),
                NetType = RoomDtoConverter.Convert(dto.NetType),
                Seats = dto.Seats,
                ComputerSeats = dto.ComputerSeats,
                HasConditioning = dto.HasConditioning,
            },
            new RoomAttachments
            {
                PdfRoomScheme = FileDtoConverter.Convert(dto.PdfRoomSchemeFile),
                Photo = FileDtoConverter.Convert(dto.PhotoFile),
            },
            dto.Owner,
            new RoomFixInfo
            {
                Status = RoomDtoConverter.Convert(dto.RoomStatus),
                FixDeadline = dto.FixDeadline,
                Comment = dto.Comment,
            },
            dto.AllowBooking);

        unitOfWork.Add(room);

        await unitOfWork.Commit(cancellationToken);

        return RoomDtoConverter.Convert(room);
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
                Type = RoomDtoConverter.Convert(dto.Type),
                Layout = RoomDtoConverter.Convert(dto.Layout),
                NetType = RoomDtoConverter.Convert(dto.NetType),
                Seats = dto.Seats,
                ComputerSeats = dto.ComputerSeats,
                HasConditioning = dto.HasConditioning
            },
            new RoomAttachments
            {
                PdfRoomScheme = FileDtoConverter.Convert(dto.PdfRoomSchemeFile),
                Photo = FileDtoConverter.Convert(dto.PhotoFile)
            },
            dto.Owner,
            new RoomFixInfo
            {
                Status = RoomDtoConverter.Convert(dto.RoomStatus),
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

        return RoomDtoConverter.Convert(roomToPatch);
    }

    private async Task<Room> GetRoomByIdInner(IUnitOfWork unitOfWork, int roomId, CancellationToken cancellationToken)
    {
        var query = queriesFactory.FindById(roomId);

        return await unitOfWork.ApplyQuery(query, cancellationToken)
               ?? throw new RoomNotFoundException($"Room [{roomId}] not found");
    }

    private async Task Validate(IUnitOfWork unitOfWork, CreateRoomDto dto, CancellationToken cancellationToken)
    {
        var query = queriesFactory.FindByName(dto.Name);
        var room = await unitOfWork.ApplyQuery(query, cancellationToken);

        if (room is not null)
        {
            throw new RoomAlreadyCreatedException($"Room with name [{dto.Name}] already exists");
        }
    }
}