using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Responses;
using WebApi.Models.Room;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class RoomModelsMapper
{
    [MapProperty(nameof(GetRoomsModel.AfterRoomId), nameof(GetRoomsRequestDto.AfterRoomId))]
    [MapProperty(nameof(GetRoomsModel.PageSize), nameof(GetRoomsRequestDto.BatchSize))]
    [MapProperty(nameof(GetRoomsModel.Page), nameof(GetRoomsRequestDto.BatchNumber), Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetRoomsRequestDto Map(GetRoomsModel dto);

    public static partial PatchRoomDto Map(PatchRoomModel model);

    public static partial CreateRoomDto Map(CreateRoomModel model);

    [MapProperty(nameof(RoomDto.Parameters.Type), nameof(PatchRoomModel.Type))]
    [MapProperty(nameof(RoomDto.Parameters.NetType), nameof(PatchRoomModel.NetType))]
    [MapProperty(nameof(RoomDto.Parameters.Layout), nameof(PatchRoomModel.Layout))]
    [MapProperty(nameof(RoomDto.Parameters.Seats), nameof(PatchRoomModel.Seats))]
    [MapProperty(nameof(RoomDto.Parameters.ComputerSeats), nameof(PatchRoomModel.ComputerSeats))]
    [MapProperty(nameof(RoomDto.Parameters.HasConditioning), nameof(PatchRoomModel.HasConditioning))]
    [MapProperty(nameof(RoomDto.Attachments.PdfRoomScheme), nameof(PatchRoomModel.PdfRoomSchemeFile))]
    [MapProperty(nameof(RoomDto.Attachments.Photo), nameof(PatchRoomModel.PhotoFile))]
    [MapProperty(nameof(RoomDto.FixInfo.Comment), nameof(PatchRoomModel.Comment))]
    [MapProperty(nameof(RoomDto.FixInfo.FixDeadline), nameof(PatchRoomModel.FixDeadline))]
    [MapProperty(nameof(RoomDto.FixInfo.Status), nameof(PatchRoomModel.RoomStatus))]
    [MapperIgnoreSource(nameof(RoomDto.Id))]
    [MapperIgnoreSource(nameof(RoomDto.Equipments))]
    [MapperIgnoreSource(nameof(RoomDto.OperatorDepartmentId))]
    public static partial PatchRoomModel MapToPatchRoomModel(RoomDto dto);

    public static partial RoomsResponseModel Map(RoomsResponseDto dto);

    public static partial RoomModel Map(RoomDto dto);
}