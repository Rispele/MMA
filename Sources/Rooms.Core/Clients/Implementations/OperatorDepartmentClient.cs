using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Implementations;

public class OperatorDepartmentClient : IOperatorDepartmentClient
{
    public Task<Dictionary<Guid, string>> GetAvailableOperators()
    {
        var response = new[] { new OperatorUserResponseDto() };
        return Task.FromResult(response.ToDictionary(x => x.Guid, x => x.FullName));
    }
}