using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Infrastructure.Clients.Implementations;

public class InstituteResponsibleClient : IInstituteResponsibleClient
{
    public Task<Dictionary<string, string>> GetAvailableInstituteResponsible()
    {
        var response = new[] { new InstituteResponsibleUserResponseDto() };
        return Task.FromResult(response.ToDictionary(keySelector: x => x.Guid.ToString(), elementSelector: x => x.FullName));
    }
}