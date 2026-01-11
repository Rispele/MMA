using WebApi.Core.Models.InternalApi;

namespace WebApi.Core.Services.Interfaces;

public interface IInternalApiService
{
    public Task SaveEdmsResolutionResult(int bookingRequestId, EdmsResolutionResult edmsResolutionResult, CancellationToken cancellationToken);
}