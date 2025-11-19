using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Implementations;

public class InstituteDepartmentClient : IInstituteDepartmentClient
{
    public Task<Dictionary<string, string>> GetAvailableInstituteDepartments()
    {
        var response = new[] { new InstituteDepartmentResponseDto() };
        return Task.FromResult(response.ToDictionary(keySelector: x => x.Id.ToString(), elementSelector: x => x.InstituteName));
    }
}