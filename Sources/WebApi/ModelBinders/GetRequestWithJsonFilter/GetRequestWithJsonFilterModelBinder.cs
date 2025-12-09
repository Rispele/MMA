using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.ModelBinders.GetRequestWithJsonFilter.Specifications;

namespace WebApi.ModelBinders.GetRequestWithJsonFilter;

file static class BinderSerializerOption
{
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}

public class GetRequestWithJsonFilterModelBinder<TRequest, TFilter, TSpecification> :
    ModelBinderAttribute<GetRequestWithJsonFilterModelBinder<TRequest, TFilter, TSpecification>>, IModelBinder
    where TSpecification : IGetRequestWithJsonFilterModelBinderSpecification<TRequest, TFilter>, new()
    where TRequest : class
    where TFilter : class
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var query = bindingContext.HttpContext.Request.Query;

        var page = ParseIntOrDefault(query["page"], defaultValue: 1);
        var pageSize = ParseIntOrDefault(query["pageSize"], defaultValue: 10);
        var afterId = ParseIntOrDefault(query["afterId"], defaultValue: 0);

        if (!query.TryGetValue("filter", out var serializedFilters) || serializedFilters.Count <= 0)
        {
            return Bind(bindingContext, page, pageSize, afterId, filter: null);
        }

        try
        {
            return Bind(
                bindingContext,
                page,
                pageSize,
                afterId,
                JsonSerializer.Deserialize<TFilter>(serializedFilters[0]!, BinderSerializerOption.SerializerOptions));
        }
        catch (JsonException exception)
        {
            bindingContext.ModelState.AddModelError(
                key: "filter",
                errorMessage: "Filter parameter is invalid JSON." + Environment.NewLine + exception.Message);
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }

    private static Task Bind(ModelBindingContext bindingContext, int page, int pageSize, int afterId, TFilter? filter)
    {
        var specification = new TSpecification();
        var request = specification.Build(page, pageSize, afterId, filter);

        bindingContext.Result = ModelBindingResult.Success(request);

        return Task.CompletedTask;
    }

    private static int ParseIntOrDefault(string? value, int defaultValue)
    {
        return int.TryParse(value, out var v) ? v : defaultValue;
    }
}