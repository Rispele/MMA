using Booking.Core.Exceptions;
using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Interfaces.Dtos.BookingRequest.Responses;
using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Core.Interfaces.Services.LkUser;
using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.BookingRequests.Mappers;
using Booking.Domain.Models.BookingRequests;
using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Services.Rooms;

namespace Booking.Core.Services.Booking.BookingRequests;

public class BookingRequestService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IRoomService roomService,
    ILkUserService lkUserService) : IBookingRequestService
{
    public async Task<BookingRequestDto> GetBookingRequestById(int bookingRequestId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await GetBookingRequestByIdInner(bookingRequestId, cancellationToken, context);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequest);
    }

    public async Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken)
    {
        var employees = await lkUserService.GetTeachers(cancellationToken);

        return employees.Select(employee => new AutocompleteEventHostResponseDto(employee.UserId, employee.FullName));
    }

    public async Task<BookingRequestsResponseDto> FilterBookingRequests(GetBookingRequestsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterBookingRequestsQuery(dto.BatchSize, dto.BatchNumber, dto.AfterBookingRequestId, dto.Filter);

        var bookingRequests = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedBookingRequests = bookingRequests.Select(BookingRequestDtoMapper.MapBookingRequestToDto).ToArray();
        int? lastBookingRequestId = convertedBookingRequests.Length == 0 ? null : convertedBookingRequests.Select(t => t.Id).Max();

        return new BookingRequestsResponseDto(convertedBookingRequests, convertedBookingRequests.Length, lastBookingRequestId);
    }

    public async Task<BookingRequestDto> CreateBookingRequest(CreateBookingRequestDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var rooms = await GetRooms(dto.RoomIds, cancellationToken);

        var bookingRequest = new BookingRequest(
            BookingRequestDtoMapper.MapBookingCreatorFromDto(dto.Creator),
            dto.Reason,
            dto.ParticipantsCount,
            dto.TechEmployeeRequired,
            dto.EventHostFullName,
            BookingRequestDtoMapper.MapRoomEventCoordinatorFromDto(dto.RoomEventCoordinator),
            dto.CreatedAt,
            dto.EventName,
            dto.BookingSchedule.Select(BookingRequestDtoMapper.MapBookingTimeFromDto).ToArray(),
            dto.Status,
            dto.ModeratorComment,
            dto.BookingScheduleStatus,
            rooms.Select(t => t.Id).ToList());

        context.Add(bookingRequest);

        await context.Commit(cancellationToken);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequest);
    }

    public async Task<BookingRequestDto> PatchBookingRequest(
        int bookingRequestId,
        PatchBookingRequestDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequestToPatch = await GetBookingRequestByIdInner(bookingRequestId, cancellationToken, context);
        var rooms = await GetRooms(dto.RoomIds, cancellationToken);

        if (rooms.Length < dto.RoomIds.Length)
        {
            var roomIdsNotFound = dto.RoomIds.Where(t => rooms.All(r => r.Id != t)).JoinStrings(", ");
            throw new InvalidRequestException($"Several rooms was not found: [{roomIdsNotFound}]");
        }

        bookingRequestToPatch.Update(
            BookingRequestDtoMapper.MapBookingCreatorFromDto(dto.Creator),
            dto.Reason,
            dto.ParticipantsCount,
            dto.TechEmployeeRequired,
            dto.EventHostFullName,
            BookingRequestDtoMapper.MapRoomEventCoordinatorFromDto(dto.RoomEventCoordinator),
            dto.CreatedAt,
            dto.EventName,
            dto.BookingSchedule.Select(BookingRequestDtoMapper.MapBookingTimeFromDto).ToArray(),
            dto.Status,
            dto.ModeratorComment,
            dto.BookingScheduleStatus);
        bookingRequestToPatch.SetRooms(rooms.Select(t => t.Id).ToList());

        await context.Commit(cancellationToken);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequestToPatch);
    }

    private async Task<RoomDto[]> GetRooms(int[] roomIds, CancellationToken cancellationToken)
    {
        var rooms = await roomService.FindRoomByIds(roomIds, cancellationToken);
        if (rooms.Length >= roomIds.Length)
        {
            return rooms;
        }

        var roomIdsNotFound = roomIds.Where(t => rooms.All(r => r.Id != t)).JoinStrings(", ");
        throw new InvalidRequestException($"Several rooms was not found: [{roomIdsNotFound}]");
    }

    private async Task<BookingRequest> GetBookingRequestByIdInner(
        int bookingRequestId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindBookingRequestByIdQuery(bookingRequestId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new BookingRequestNotFoundException($"BookingRequest [{bookingRequestId}] not found");
    }
}