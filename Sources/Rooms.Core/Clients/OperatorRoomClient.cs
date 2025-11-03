using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients;

public class OperatorRoomClient : IOperatorRoomClient
{
    public Task<Dictionary<Guid, string>> GetAvailableOperators()
    {
        var response = new[] { new OperatorUserResponseDto() };
        return Task.FromResult(response.ToDictionary(x => x.Guid, x => x.FullName));
    }
}