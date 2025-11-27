using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.BookingRequest;
using Rooms.Core.Dtos.BookingRequest.Requests;
using Rooms.Core.Dtos.BookingRequest.Responses;
using Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;
using WebApi.Models.BookingRequest;
using WebApi.Models.BookingRequest.RoomEventCoordinator;
using WebApi.Models.Requests.BookingRequests;
using WebApi.Models.Responses;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookingRequestModelsMapper
{
    public static partial BookingRequestModel MapBookingRequestToModel(BookingRequestDto bookingRequest);

    [MapperIgnoreSource(nameof(BookingRequestDto.Id))]
    [MapProperty(nameof(BookingRequestDto.RoomEventCoordinator), nameof(PatchBookingRequestModel.RoomEventCoordinator), Use = nameof(MapRoomEventCoordinatorFromDto))]
    public static partial PatchBookingRequestModel MapBookingRequestToPatchModel(BookingRequestDto bookingRequest);

    [MapProperty(nameof(GetBookingRequestsModel.AfterBookingRequestId), nameof(GetBookingRequestsDto.AfterBookingRequestId))]
    [MapProperty(nameof(GetBookingRequestsModel.PageSize), nameof(GetBookingRequestsDto.BatchSize))]
    [MapProperty(
        nameof(GetBookingRequestsModel.Page),
        nameof(GetBookingRequestsDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetBookingRequestsDto MapGetBookingRequestFromModel(GetBookingRequestsModel model);
    
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
