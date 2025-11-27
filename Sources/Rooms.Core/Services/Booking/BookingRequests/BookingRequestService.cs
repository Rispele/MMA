using Commons;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.BookingRequest.Requests;
using Rooms.Core.Dtos.BookingRequest.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.BookingRequest;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Core.Services.Booking.BookingRequests.Interfaces;
using Rooms.Core.Services.Booking.BookingRequests.Mappers;
using Rooms.Core.Services.LkUser;
using Rooms.Core.Services.Rooms.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Services.Booking.BookingRequests;

public class BookingRequestService(
    IUnitOfWorkFactory unitOfWorkFactory,
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

        var rooms = await (await context.ApplyQuery(new FindRoomsByIdQuery(dto.RoomIds.ToArray()), cancellationToken)).ToListAsync(cancellationToken);

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
            rooms);

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
        var rooms = await (await context.ApplyQuery(new FindRoomsByIdQuery(dto.RoomIds.ToArray()), cancellationToken)).ToListAsync(cancellationToken);

        if (rooms.Count < dto.RoomIds.Length)
        {
            var roomIdsNotFound = dto.RoomIds.Where(t => rooms.All(r => r.Id != t)).JoinStrings(", ");
            throw new RoomNotFoundException($"Several rooms was not found: [{roomIdsNotFound}]");
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
        bookingRequestToPatch.SetRooms(rooms);

        await context.Commit(cancellationToken);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequestToPatch);
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