using Commons;
using Rooms.Core.Clients.Interfaces;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Services.Implementations;

public class BookingRequestService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IBookingRequestQueryFactory bookingRequestQueryFactory,
    IRoomService roomService,
    IEventHostClient eventHostClient) : IBookingRequestService
{
    public async Task<BookingRequestDto> GetBookingRequestById(int bookingRequestId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await GetBookingRequestByIdInner(bookingRequestId, cancellationToken, context);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequest);
    }

    public async Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken)
    {
        var autocompleteNames = await eventHostClient.AutocompleteEventHostName(name, cancellationToken);

        return autocompleteNames;
    }

    public async Task<BookingRequestsResponseDto> FilterBookingRequests(GetBookingRequestsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = bookingRequestQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterBookingRequestId, dto.Filter);

        var bookingRequests = await context
            .ApplyQuery(query)
            .ToListAsync(cancellationToken);

        var convertedBookingRequests = bookingRequests.Select(BookingRequestDtoMapper.MapBookingRequestToDto).ToArray();
        int? lastBookingRequestId = convertedBookingRequests.Length == 0 ? null : convertedBookingRequests.Select(t => t.Id).Max();

        return new BookingRequestsResponseDto(convertedBookingRequests, convertedBookingRequests.Length, lastBookingRequestId);
    }

    public async Task<BookingRequestDto> CreateBookingRequest(CreateBookingRequestDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var rooms = await roomService.GetRoomsById(dto.RoomIds, cancellationToken);

        var bookingRequest = new BookingRequest(
            BookingRequestDtoMapper.MapBookingCreatorFromDto(dto.Creator),
            dto.Reason,
            dto.ParticipantsCount,
            dto.TechEmployeeRequired,
            dto.EventHostFullName,
            dto.EventType,
            dto.CoordinatorInstitute,
            dto.CoordinatorFullName,
            dto.CreatedAt,
            dto.EventName,
            // rooms.Select(RoomDtoMapper.Convert),
            dto.BookingSchedule.Select(BookingRequestDtoMapper.MapBookingTimeFromDto).ToArray(),
            dto.Status,
            dto.ModeratorComment,
            dto.BookingScheduleStatus);

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
        var rooms = await roomService.GetRoomsById(dto.RoomIds, cancellationToken);

        bookingRequestToPatch.Update(
            BookingRequestDtoMapper.MapBookingCreatorFromDto(dto.Creator),
            dto.Reason,
            dto.ParticipantsCount,
            dto.TechEmployeeRequired,
            dto.EventHostFullName,
            dto.EventType,
            dto.CoordinatorInstitute,
            dto.CoordinatorFullName,
            dto.CreatedAt,
            dto.EventName,
            // rooms.Select(RoomDtoMapper.Convert),
            dto.BookingSchedule.Select(BookingRequestDtoMapper.MapBookingTimeFromDto).ToArray(),
            dto.Status,
            dto.ModeratorComment,
            dto.BookingScheduleStatus);

        await context.Commit(cancellationToken);

        return BookingRequestDtoMapper.MapBookingRequestToDto(bookingRequestToPatch);
    }

    private async Task<BookingRequest> GetBookingRequestByIdInner(
        int bookingRequestId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = bookingRequestQueryFactory.FindById(bookingRequestId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new BookingRequestNotFoundException($"BookingRequest [{bookingRequestId}] not found");
    }
}