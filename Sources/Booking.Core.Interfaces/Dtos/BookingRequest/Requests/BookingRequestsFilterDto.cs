using Booking.Domain.Propagated.BookingRequests;
using Commons.Core.Models.Filtering;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.Requests;

public record BookingRequestsFilterDto
{
    public FilterParameterDto<string>? CreatedAt { get; init; }
    public FilterParameterDto<string>? EventName { get; init; }

    public FilterMultiParameterDto<BookingStatus>? Status { get; init; }

    // public FilterParameterDto<string>? ModeratorComment { get; init; }
    public FilterMultiParameterDto<BookingScheduleStatus>? BookingScheduleStatus { get; init; }
    public FilterMultiParameterDto<int>? Rooms { get; init; }
}