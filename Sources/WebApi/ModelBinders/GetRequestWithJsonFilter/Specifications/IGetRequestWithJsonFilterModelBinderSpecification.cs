namespace WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

public interface IGetRequestWithJsonFilterModelBinderSpecification<out TRequest, in TFilter>
    where TRequest : class
    where TFilter : class
{
    public TRequest Build(int pageNumber, int pageSize, int afterId, TFilter? filter);
}