using WebApi.ModelConverters;
using WebApi.Models.BookingRequest;
using WebApi.Models.Requests.BookingRequests;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreBookingRequestService = Rooms.Core.Services.Interfaces.IBookingRequestService;

namespace WebApi.Services.Implementations;

public class BookingRequestService(ICoreBookingRequestService bookingRequestService) : IBookingRequestService
{
    public async Task<BookingRequestsResponseModel> GetBookingRequestsAsync(
        GetBookingRequestsModel model,
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