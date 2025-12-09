using WebApi.Core.Models.BookingRequest;

namespace WebApi.Core.Models.Responses;

public record BookingRequestsResponseModel
{
    public BookingRequestModel[] BookingRequests { get; init; } = [];
    public int Count { get; init; }
}