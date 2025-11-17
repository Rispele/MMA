using WebApi.Models.BookingRequest;
using WebApi.Models.Requests.BookingRequests;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IBookingRequestService
{
    Task<BookingRequestsResponseModel> GetBookingRequestsAsync(GetBookingRequestsModel model, CancellationToken cancellationToken);
    Task<BookingRequestModel> GetBookingRequestByIdAsync(int id, CancellationToken cancellationToken);
    Task<BookingRequestModel> CreateBookingRequestAsync(CreateBookingRequestModel model, CancellationToken cancellationToken);
    Task<PatchBookingRequestModel> GetBookingRequestPatchModel(int bookingRequestId, CancellationToken cancellationToken);
    Task<BookingRequestModel> PatchBookingRequestAsync(
        int bookingRequestId,
        PatchBookingRequestModel request,
        CancellationToken cancellationToken);
}