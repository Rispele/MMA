using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.InternalApi;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Controllers;

/// <summary>
/// Экономимим ресурсы. Internal Api поднимаем под вебапи
/// </summary>
[Controller]
public class InternalApiController(IInternalApiService internalApiService) : ControllerBase
{
    public Task SaveEdmsResolutionResult(int bookingRequestId, EdmsResolutionResult resolutionResult, CancellationToken cancellationToken)
    {
        return internalApiService.SaveEdmsResolutionResult(bookingRequestId, resolutionResult, cancellationToken);
    }
}