using Booking.Core.Interfaces.Dtos.BookingRequest;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Interfaces.Dtos.BookingRequest.Responses;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Riok.Mapperly.Abstractions;
using WebApi.Core.Models.BookingRequest;
using WebApi.Core.Models.BookingRequest.RoomEventCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.BookingRequests;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestModelsMapper
{
    public static partial BookingRequestModel MapBookingRequestToModel(BookingRequestDto bookingRequest);

    [MapperIgnoreSource(nameof(BookingRequestDto.Id))]
    [MapProperty(nameof(BookingRequestDto.RoomEventCoordinator), nameof(PatchBookingRequestModel.RoomEventCoordinator), Use = nameof(MapRoomEventCoordinatorFromDto))]
    public static partial PatchBookingRequestModel MapBookingRequestToPatchModel(BookingRequestDto bookingRequest);

    [MapProperty(nameof(GetRequest<BookingRequestsFilterModel>.PageSize), nameof(GetBookingRequestsDto.BatchSize))]
    [MapProperty(
        source: nameof(GetRequest<BookingRequestsFilterModel>.Page),
        target: nameof(GetBookingRequestsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetBookingRequestsDto MapGetBookingRequestFromModel(GetRequest<BookingRequestsFilterModel> model);

    [MapProperty(nameof(CreateBookingRequestModel.RoomEventCoordinator), nameof(CreateBookingRequestDto.RoomEventCoordinator), Use = nameof(MapRoomEventCoordinatorToDto))]
    public static partial CreateBookingRequestDto MapCreateBookingRequestFromModel(CreateBookingRequestModel bookingRequest);

    [MapProperty(nameof(BookingRequestDto.RoomEventCoordinator), nameof(BookingRequestModel.RoomEventCoordinator), Use = nameof(MapRoomEventCoordinatorToDto))]
    public static partial PatchBookingRequestDto MapPatchBookingRequestFromModel(PatchBookingRequestModel bookingRequest);

    public static partial AutocompleteEventHostResponseModel MapEventHostToDto(AutocompleteEventHostResponseDto eventHost);

    public static IRoomEventCoordinatorModel MapRoomEventCoordinatorFromDto(IRoomEventCoordinatorDto coordinator)
    {
        return coordinator switch
        {
            OtherRoomEventCoordinatorDto => new OtherRoomEventCoordinatorModel(),
            ScientificRoomEventCoordinatorDto scientificRoomEventCoordinatorDto => new ScientificRoomEventCoordinatorModel(
                scientificRoomEventCoordinatorDto.CoordinatorDepartmentId,
                scientificRoomEventCoordinatorDto.CoordinatorId),
            StudentRoomEventCoordinatorDto => new StudentRoomEventCoordinatorModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(coordinator))
        };
    }

    public static IRoomEventCoordinatorDto MapRoomEventCoordinatorToDto(IRoomEventCoordinatorModel coordinator)
    {
        return coordinator switch
        {
            OtherRoomEventCoordinatorModel => new OtherRoomEventCoordinatorDto(),
            ScientificRoomEventCoordinatorModel scientificRoomEventCoordinatorDto => new ScientificRoomEventCoordinatorDto(
                scientificRoomEventCoordinatorDto.CoordinatorDepartmentId,
                scientificRoomEventCoordinatorDto.CoordinatorId),
            StudentRoomEventCoordinatorModel => new StudentRoomEventCoordinatorDto(),
            _ => throw new ArgumentOutOfRangeException(nameof(coordinator))
        };
    }
}
