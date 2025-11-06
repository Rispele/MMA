using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Requests.Rooms;
using WebApi.Models.Requests.Rooms;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class RoomsModelsConverter
{
    public static partial PatchRoomDto Map(PatchRoomModel model);

    [MapProperty(nameof(GetRoomsModel.AfterRoomId), nameof(GetRoomsRequestDto.AfterRoomId))]
    [MapProperty(nameof(GetRoomsModel.PageSize), nameof(GetRoomsRequestDto.BatchSize))]
    [MapProperty(nameof(GetRoomsModel.Page), nameof(GetRoomsRequestDto.BatchNumber), Use = nameof(MapPageNumberToBatchNumber))]
    public static partial GetRoomsRequestDto Map(GetRoomsModel dto);

    public static partial CreateRoomDto Map(CreateRoomModel model);

    [UserMapping(Default = false)]
    private static int MapPageNumberToBatchNumber(int pageNumber)
    {
        return Math.Max(pageNumber - 1, 0);
    }
}