using System.ComponentModel.DataAnnotations;

namespace WebApi.Core.Models.Requests;

public record GetRequest<TFilter>
{
    [Range(minimum: 1, int.MaxValue, ErrorMessage = "Номер страницы не может быть меньше 1")]
    public int Page { get; init; }

    [Range(minimum: 10, maximum: 100, ErrorMessage = "Размер страницы не может быть меньше 10 и больше 100")]
    public int PageSize { get; init; }

    public TFilter? Filter { get; init; }
}