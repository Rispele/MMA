using Rooms.Domain.Models.BookingRequests;
using WebApi.Models.Requests.Filtering;

namespace WebApi.Models.Requests.BookingRequests;

public record BookingRequestsFilterModel
{
    public FilterParameterModel<string>? CreatedAt { get; init; }
    public FilterParameterModel<string>? EventName { get; init; }
    public FilterMultiParameterModel<BookingStatus>? Status { get; init; }
    public FilterParameterModel<string>? ModeratorComment { get; init; }
    public FilterMultiParameterModel<BookingScheduleStatus>? BookingScheduleStatus { get; init; }
}