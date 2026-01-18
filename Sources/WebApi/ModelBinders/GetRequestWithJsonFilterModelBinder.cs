using System.Text.Json;
using Commons.Optional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Core.Models.Requests;

namespace WebApi.ModelBinders;

file static class BinderSerializerOption
{
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}

public class GetRequestWithJsonFilterModelBinder<TFilter> : ModelBinderAttribute<GetRequestWithJsonFilterModelBinder<TFilter>>, IModelBinder
    where TFilter : class
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var query = bindingContext.HttpContext.Request.Query;

        string? filter = null;
        if (query.TryGetValue("filter", out var serializedFilters) && serializedFilters.Count > 0)
        {
            filter = serializedFilters[0];
        }

        bindingContext.Result = await BindModelInner(
            page: query["page"],
            pageSize: query["pageSize"],
            filter,
            bindingContext.ModelState.AddModelError);
    }

    public Task<ModelBindingResult> BindModelInner(
        string? page,
        string? pageSize,
        string? filter,
        Action<string, string> addModelError)
    {
        var pageParsed = ParseIntOrDefault(page, defaultValue: 1);
        var pageSizeParsed = ParseIntOrDefault(pageSize, defaultValue: 10);

        try
        {
            return Bind(
                pageParsed,
                pageSizeParsed,
                filter.AsOptional().Map(t => JsonSerializer.Deserialize<TFilter>(t, BinderSerializerOption.SerializerOptions)));
        }
        catch (JsonException exception)
        {
            addModelError("filter", "Filter parameter is invalid JSON." + Environment.NewLine + exception.Message);
            return Task.FromResult(ModelBindingResult.Failed());
        }
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

    private static int ParseIntOrDefault(string? value, int defaultValue)
    {
        return int.TryParse(value, out var v) ? v : defaultValue;
    }
}