using WebApi.Models.InstituteResponsible;

namespace WebApi.Models.Requests.InstituteResponsible;

public record PatchInstituteResponsibleModel
{
    public required string Institute { get; init; } 
    public required CoordinatorModel[] Coordinators { get; init; }
}