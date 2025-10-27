using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests.EquipmentSchemas;

public record GetEquipmentSchemasModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Номер страницы не может быть меньше 1")]
    public int Page { get; init; }

    [Range(10, 100, ErrorMessage = "Размер страницы не может быть меньше 10 и больше 100")]
    public int PageSize { get; init; }

    public int AfterEquipmentSchemaId { get; init; }
    public EquipmentSchemasFilterModel? Filter { get; init; }
}