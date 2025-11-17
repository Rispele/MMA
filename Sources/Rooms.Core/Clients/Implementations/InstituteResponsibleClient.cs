using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Implementations;

public class InstituteResponsibleClient : IInstituteResponsibleClient
{
    public Task<Dictionary<string, string>> GetAvailableInstituteResponsible()
    {
        var response = new[] { new InstituteResponsibleUserResponseDto { Id = Guid.NewGuid().ToString(), FullName = Guid.NewGuid().ToString() } };
        return Task.FromResult(response.ToDictionary(x => x.Id, x => x.FullName));
    }
}