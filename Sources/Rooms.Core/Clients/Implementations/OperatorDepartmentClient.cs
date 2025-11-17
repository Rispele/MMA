using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Implementations;

public class OperatorDepartmentClient : IOperatorDepartmentClient
{
    public Task<Dictionary<string, string>> GetAvailableOperators()
    {
        var response = new[] { new OperatorUserResponseDto { Id = Guid.NewGuid().ToString(), FullName = Guid.NewGuid().ToString() } };
        return Task.FromResult(response.ToDictionary(x => x.Id, x => x.FullName));
    }
}