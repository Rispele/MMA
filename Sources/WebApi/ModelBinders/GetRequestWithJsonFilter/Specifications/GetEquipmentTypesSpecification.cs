using WebApi.Models.Requests.EquipmentTypes;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public class GetEquipmentTypesSpecification : IGetRequestWithJsonFilterModelBinderSpecification<GetEquipmentTypesModel, EquipmentTypesFilterModel>
{
    public GetEquipmentTypesModel Build(int pageNumber, int pageSize, int afterId, EquipmentTypesFilterModel? filter)
    {
        return new GetEquipmentTypesModel
        {
            AfterId = afterId,
            Filter = filter,
            PageSize = pageSize,
            Page = pageNumber
        };
    }
}