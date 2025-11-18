using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using WebApi.Models.BookingRequest;
using WebApi.Models.Requests.BookingRequests;
using WebApi.Models.Responses;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestModelsMapper
{
    [MapProperty(nameof(BookingRequestDto.Rooms), nameof(BookingRequestModel.RoomIds), Use = nameof(MapRoomIds))]
    public static partial BookingRequestModel MapBookingRequestToModel(BookingRequestDto bookingRequest);

    [MapperIgnoreSource(nameof(BookingRequestDto.Id))]
    [MapProperty(nameof(BookingRequestDto.Rooms), nameof(BookingRequestModel.RoomIds), Use = nameof(MapRoomIds))]
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

    public static partial AutocompleteEventHostResponseModel MapEventHostToDto(AutocompleteEventHostResponseDto eventHost);

    public static IEnumerable<int> MapRoomIds(IEnumerable<RoomDto> rooms)
    {
        return rooms.Select(x => x.Id);
    }
}
