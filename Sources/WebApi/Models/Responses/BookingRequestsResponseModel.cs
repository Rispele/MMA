using WebApi.Models.BookingRequest;

namespace WebApi.Models.Responses;

public record BookingRequestsResponseModel
{
    public BookingRequestModel[] BookingRequests { get; init; } = [];
    public int Count { get; init; }
}