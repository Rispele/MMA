using Rooms.Core.Dtos.OperatorRoom;
using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Interfaces;

public interface IOperatorRoomService
{
    Task<OperatorRoomDto> GetOperatorRoomById(int operatorRoomId, CancellationToken cancellationToken);
    Task<OperatorRoomsResponseDto> FilterOperatorRooms(GetOperatorRoomsDto dto, CancellationToken cancellationToken);
    Task<OperatorRoomDto> CreateOperatorRoom(CreateOperatorRoomDto dto, CancellationToken cancellationToken);
    Task<OperatorRoomDto> PatchOperatorRoom(int operatorRoomId, PatchOperatorRoomDto dto, CancellationToken cancellationToken);
}