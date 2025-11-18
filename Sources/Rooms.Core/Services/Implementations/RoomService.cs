using Commons;
using Commons.Optional;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using Rooms.Core.ExcelExporters.Exporters;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Models.Room.Fix;
using Rooms.Domain.Models.Room.Parameters;
using FileDtoConverter = Rooms.Core.DtoConverters.FileDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class RoomService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IRoomQueriesFactory queriesFactory) : IRoomService
{
    public async Task<RoomDto> GetRoomById(int roomId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var room = await GetRoomByIdInner(unitOfWork, roomId, cancellationToken);

        return room.Map(RoomDtoMapper.Convert);
    }

    public async Task<IEnumerable<RoomDto>> GetRoomsById(IEnumerable<int> roomIds, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var rooms = await GetRoomsByIdInner(unitOfWork, roomIds, cancellationToken);

        return rooms.Select(x => x.Map(RoomDtoMapper.Convert));
    }

    public async Task<RoomsResponseDto> FilterRooms(GetRoomsRequestDto requestDto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var query = queriesFactory.Filter(requestDto.BatchSize, requestDto.BatchNumber, requestDto.AfterRoomId, requestDto.Filter);

        var rooms = await unitOfWork
            .ApplyQuery(query)
            .ToListAsync(cancellationToken);

        var convertedRooms = rooms.Select(RoomDtoMapper.Convert).ToArray();
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
                Type = RoomDtoMapper.Convert(dto.Type),
                Layout = RoomDtoMapper.Convert(dto.Layout),
                NetType = RoomDtoMapper.Convert(dto.NetType),
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
                Status = RoomDtoMapper.Convert(dto.RoomStatus),
                FixDeadline = dto.FixDeadline,
                Comment = dto.Comment,
            },
            dto.AllowBooking);

        unitOfWork.Add(room);

        await unitOfWork.Commit(cancellationToken);

        return RoomDtoMapper.Convert(room);
    }

    public async Task<RoomDto> PatchRoom(int roomId, PatchRoomDto dto, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var roomToPatch = await GetRoomByIdInner(unitOfWork, roomId, cancellationToken);

        roomToPatch.Update(
            dto.Name,
            dto.Description,
            dto.ScheduleAddress.AsOptional().Map(t => new RoomScheduleAddress
            {
                RoomNumber = t.RoomNumber,
                Address = t.Address,
            }),
            new RoomParameters
            {
                Type = RoomDtoMapper.Convert(dto.Type),
                Layout = RoomDtoMapper.Convert(dto.Layout),
                NetType = RoomDtoMapper.Convert(dto.NetType),
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
                Status = RoomDtoMapper.Convert(dto.RoomStatus),
                FixDeadline = dto.FixDeadline,
                Comment = dto.Comment
            },
            dto.AllowBooking);

        await unitOfWork.Commit(cancellationToken);

        return RoomDtoMapper.Convert(roomToPatch);
    }

    public async Task<FileExportDto> ExportRoomRegistry(CancellationToken cancellationToken)
    {
        var exportDtos = new[]
        {
            new RoomRegistryExcelExportDto
            {
                Name = Guid.NewGuid().ToString()
            }
        };
        var exporter = new RoomRegistryExcelExporter();
        return exporter.Export(exportDtos, cancellationToken);
    }

    private async Task<Room> GetRoomByIdInner(IUnitOfWork unitOfWork, int roomId, CancellationToken cancellationToken)
    {
        var query = queriesFactory.FindById(roomId);

        return await unitOfWork.ApplyQuery(query, cancellationToken)
               ?? throw new RoomNotFoundException($"Room [{roomId}] not found");
    }

    private async Task<IEnumerable<Room>> GetRoomsByIdInner(IUnitOfWork unitOfWork, IEnumerable<int> roomIds, CancellationToken cancellationToken)
    {
        var query = queriesFactory.FindByIds(roomIds);

        var rooms = unitOfWork.ApplyQuery(query).WithCancellation(cancellationToken);
        var result = new List<Room>();

        await foreach (var room in rooms)
        {
            result.Add(room);
        }
        return result;
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