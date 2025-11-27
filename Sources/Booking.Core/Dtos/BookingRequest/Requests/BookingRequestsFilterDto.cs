using Rooms.Core.Dtos.Filtering;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Dtos.BookingRequest.Requests;

public record BookingRequestsFilterDto
{
    public FilterParameterDto<string>? CreatedAt { get; init; }
    public FilterParameterDto<string>? EventName { get; init; }

    public FilterMultiParameterDto<BookingStatus>? Status { get; init; }

    // public FilterParameterDto<string>? ModeratorComment { get; init; }
    public FilterMultiParameterDto<BookingScheduleStatus>? BookingScheduleStatus { get; init; }
    public FilterMultiParameterDto<int>? Rooms { get; init; }
}