using WebApi.Models.Requests.EquipmentSchemas;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetEquipmentSchemasSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetEquipmentSchemasModel, EquipmentSchemasFilterModel>
{
    public GetEquipmentSchemasModel Build(int pageNumber, int pageSize, int afterId, EquipmentSchemasFilterModel? filter)
    {
        return new GetEquipmentSchemasModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}