using WebApi.Models.InstituteResponsible;

namespace WebApi.Models.Requests.InstituteResponsible;

public record CreateInstituteResponsibleModel
{
    public required string Institute { get; init; }
    public required CoordinatorModel[] Coordinators { get; init; }
}