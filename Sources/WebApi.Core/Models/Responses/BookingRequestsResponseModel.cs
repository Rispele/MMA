using WebApi.Core.Models.BookingRequest;

namespace WebApi.Core.Models.Responses;

public record BookingRequestsResponseModel(BookingRequestModel[] BookingRequests, int TotalCount);