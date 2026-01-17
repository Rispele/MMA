using WebApi.Core.Models.Room;

namespace WebApi.Core.Models.Responses;

public record RoomsResponseModel(RoomModel[] Rooms, int TotalCount);