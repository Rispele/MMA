using Booking.Domain.Propagated.BookingRequests;
using WebApi.Core.Models.Requests.Filtering;

namespace WebApi.Core.Models.Requests.BookingRequests;

public record BookingRequestsFilterModel
{
    public FilterParameterModel<string>? CreatedAt { get; init; }
    public FilterParameterModel<string>? EventName { get; init; }
    public FilterMultiParameterModel<BookingStatus>? Status { get; init; }
    // public FilterParameterModel<string>? ModeratorComment { get; init; }
    public FilterMultiParameterModel<BookingScheduleStatus>? BookingScheduleStatus { get; init; }
    public FilterMultiParameterModel<int>? Rooms { get; init; }
}