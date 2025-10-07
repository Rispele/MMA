namespace Application.Clients.Dtos.Requests.RoomsQuerying;

public record GetRoomsRequest(int BatchNumber, int BatchSize, int AfterRoomId, RoomsFilterDto? Filter);
