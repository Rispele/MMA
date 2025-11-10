using WebApi.Models.InstituteResponsible;

namespace WebApi.Models.Responses;

public record InstituteResponsibleResponseModel
{
    public InstituteResponsibleModel[] InstituteResponsible { get; init; } = [];
    public int Count { get; init; }
}