using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests.InstituteResponsible;

public record GetInstituteCoordinatorModel
{
    [Range(minimum: 1, int.MaxValue, ErrorMessage = "Номер страницы не может быть меньше 1")]
    public int Page { get; init; }

    [Range(minimum: 10, maximum: 100, ErrorMessage = "Размер страницы не может быть меньше 10 и больше 100")]
    public int PageSize { get; init; }

    public int AfterInstituteResponsibleId { get; init; }
    public InstituteResponsibleFilterModel? Filter { get; init; }
}