using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Infrastructure.Clients.Implementations;

public class EventHostClient : IEventHostClient
{
    public async Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken)
    {
        var response = (IEnumerable<AutocompleteEventHostResponseDto>)new List<AutocompleteEventHostResponseDto>() { new() };
        return response;
    }
}