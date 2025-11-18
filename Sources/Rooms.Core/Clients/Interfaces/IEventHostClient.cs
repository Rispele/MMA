using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Clients.Interfaces;

public interface IEventHostClient
{
    Task<IEnumerable<AutocompleteEventHostResponseDto>> AutocompleteEventHostName(string name, CancellationToken cancellationToken);
}