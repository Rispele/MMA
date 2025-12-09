using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests.Equipments;

public record GetEquipmentsModel
{
    [Range(minimum: 1, int.MaxValue, ErrorMessage = "Номер страницы не может быть меньше 1")]
    public int Page { get; init; }

    [Range(minimum: 10, maximum: 100, ErrorMessage = "Размер страницы не может быть меньше 10 и больше 100")]
    public int PageSize { get; init; }

    public int AfterId { get; init; }
    public EquipmentsFilterModel? Filter { get; init; }
}