using WebApi.Core.Models.BookingRequest;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.BookingRequests;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IBookingRequestService
{
    Task<BookingRequestsResponseModel> GetBookingRequestsAsync(GetRequest<BookingRequestsFilterModel> model, CancellationToken cancellationToken);
    Task<BookingRequestModel> GetBookingRequestByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<AutocompleteEventHostResponseModel>> AutocompleteEventHostNameAsync(string name, CancellationToken cancellationToken);
    Task<BookingRequestModel> CreateBookingRequestAsync(CreateBookingRequestModel model, CancellationToken cancellationToken);
    Task<PatchBookingRequestModel> GetBookingRequestPatchModel(int bookingRequestId, CancellationToken cancellationToken);

    Task<BookingRequestModel> PatchBookingRequestAsync(
        int bookingRequestId,
        PatchBookingRequestModel request,
        CancellationToken cancellationToken);
}