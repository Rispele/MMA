using System.Text.Json;
using Commons.Optional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Core.Models.Requests;

namespace WebApi.ModelBinders;

public class GetRequestWithJsonFilterModelBinder<TFilter> : ModelBinderAttribute<GetRequestWithJsonFilterModelBinder<TFilter>>, IModelBinder
    where TFilter : class
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var query = bindingContext.HttpContext.Request.Query;

        query.TryGetValue("model", out var queryValue);
        var stringValue = queryValue.ToString();
        var formattedStringValue = stringValue.Substring(1, stringValue.Length - 2).Replace("\\\"", "\"");
        try
        {
            var parsedValue = JsonSerializer.Deserialize<GetRequest<TFilter>>(formattedStringValue, JsonSerializerOptions.Web);
            bindingContext.Result = await BindModelInner(
                page: parsedValue!.Page,
                pageSize: parsedValue.PageSize,
                filter: parsedValue.Filter);
        }
        catch (JsonException exception)
        {
            bindingContext.ModelState.AddModelError("filter", "Filter parameter is invalid JSON." + Environment.NewLine + exception.Message);
            bindingContext.Result = ModelBindingResult.Failed();
        }
    }

    public Task<ModelBindingResult> BindModelInner(
        int page,
        int pageSize,
        TFilter? filter)
    {
        return Bind(page, pageSize, filter.AsOptional());
    }

    private static Task<ModelBindingResult> Bind(int page, int pageSize, TFilter? filter)
    {
        var request = new GetRequest<TFilter>
        {
            Page = page,
            PageSize = pageSize,
            Filter = filter
        };

        return Task.FromResult(ModelBindingResult.Success(request));
    }
}