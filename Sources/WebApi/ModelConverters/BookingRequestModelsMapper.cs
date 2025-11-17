using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.Requests.BookingRequests;
using WebApi.Models.BookingRequest;
using WebApi.Models.Requests.BookingRequests;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestModelsMapper
{
    public static partial BookingRequestModel MapBookingRequestToModel(BookingRequestDto bookingRequest);

    public static partial PatchBookingRequestModel MapBookingRequestToPatchModel(BookingRequestDto bookingRequest);

    [MapProperty(nameof(GetBookingRequestsModel.AfterBookingRequestId), nameof(GetBookingRequestsDto.AfterBookingRequestId))]
    [MapProperty(nameof(GetBookingRequestsModel.PageSize), nameof(GetBookingRequestsDto.BatchSize))]
    [MapProperty(
        nameof(GetBookingRequestsModel.Page),
        nameof(GetBookingRequestsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetBookingRequestsDto MapGetBookingRequestFromModel(GetBookingRequestsModel model);

    public static partial CreateBookingRequestDto MapCreateBookingRequestFromModel(CreateBookingRequestModel bookingRequest);

    public static partial PatchBookingRequestDto MapPatchBookingRequestFromModel(PatchBookingRequestModel bookingRequest);
}
