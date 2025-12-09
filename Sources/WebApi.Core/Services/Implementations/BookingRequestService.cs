using WebApi.Core.ModelConverters;
using WebApi.Core.Models.BookingRequest;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.BookingRequests;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class BookingRequestService(Booking.Core.Interfaces.Services.BookingRequests.IBookingRequestService bookingRequestService) : IBookingRequestService
{
    public async Task<BookingRequestsResponseModel> GetBookingRequestsAsync(
        GetRequest<BookingRequestsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var getBookingRequestsRequest = BookingRequestModelsMapper.MapGetBookingRequestFromModel(model);

        var batch = await bookingRequestService.FilterBookingRequests(getBookingRequestsRequest, cancellationToken);

        return new BookingRequestsResponseModel
        {
            BookingRequests = batch.BookingRequests.Select(BookingRequestModelsMapper.MapBookingRequestToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<BookingRequestModel> GetBookingRequestByIdAsync(int id, CancellationToken cancellationToken)
    {
        var bookingRequest = await bookingRequestService.GetBookingRequestById(id, cancellationToken);

        return BookingRequestModelsMapper.MapBookingRequestToModel(bookingRequest);
    }

    public async Task<IEnumerable<AutocompleteEventHostResponseModel>> AutocompleteEventHostNameAsync(string name, CancellationToken cancellationToken)
    {
        var autocompleteNames = await bookingRequestService.AutocompleteEventHostName(name, cancellationToken);

        return autocompleteNames.Select(BookingRequestModelsMapper.MapEventHostToDto);
    }

    public async Task<BookingRequestModel> CreateBookingRequestAsync(
        CreateBookingRequestModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = BookingRequestModelsMapper.MapCreateBookingRequestFromModel(model);

        var bookingRequest = await bookingRequestService.CreateBookingRequest(innerRequest, cancellationToken);

        return BookingRequestModelsMapper.MapBookingRequestToModel(bookingRequest);
    }

    public async Task<PatchBookingRequestModel> GetBookingRequestPatchModel(int bookingRequestId, CancellationToken cancellationToken)
    {
        var bookingRequest = await bookingRequestService.GetBookingRequestById(bookingRequestId, cancellationToken);

        return BookingRequestModelsMapper.MapBookingRequestToPatchModel(bookingRequest);
    }

    public async Task<BookingRequestModel> PatchBookingRequestAsync(
        int bookingRequestId,
        PatchBookingRequestModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = BookingRequestModelsMapper.MapPatchBookingRequestFromModel(patchModel);

        var patched = await bookingRequestService.PatchBookingRequest(bookingRequestId, patchRequest, cancellationToken);

        return BookingRequestModelsMapper.MapBookingRequestToModel(patched);
    }
}