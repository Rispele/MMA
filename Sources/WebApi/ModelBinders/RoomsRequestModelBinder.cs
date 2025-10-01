using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests;

namespace WebApi.ModelBinders;

/// <summary>
/// Simple model binder: reads page/pageSize/afterRoomId from query,
/// and reads a "filter" query parameter which contains JSON for RoomsFilter.
/// This is practical and keeps nested filter mapping simple from frontend.
/// </summary>
public class GetRoomsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], 10);
        var afterRoomId = ParseIntOrDefault(q["afterRoomId"], 0);

        RoomsFilter? filter = null;
        if (q.TryGetValue("filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<RoomsFilter>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException)
            {
                bindingContext.ModelState.AddModelError("filter", "Filter parameter is invalid JSON.");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
        }

        var result = new RoomsRequest
        {
            Page = page,
            PageSize = pageSize,
            AfterRoomId = afterRoomId,
            Filter = filter
        };

        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;

        static int ParseIntOrDefault(string? value, int defaultValue)
            => int.TryParse(value, out var v) ? v : defaultValue;
    }
}