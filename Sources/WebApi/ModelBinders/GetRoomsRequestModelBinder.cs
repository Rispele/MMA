using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests.Rooms;

namespace WebApi.ModelBinders;

/// <summary>
///     Simple model binder: reads page/pageSize/afterRoomId from query,
///     and reads a "filter" query parameter which contains JSON for RoomsFilterModel.
///     This is practical and keeps nested filter mapping simple from frontend.
/// </summary>
public class GetRoomsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], defaultValue: 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], defaultValue: 10);
        var afterRoomId = ParseIntOrDefault(q["afterRoomId"], defaultValue: 0);

        RoomsFilterModel? filter = null;
        if (q.TryGetValue(key: "filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<RoomsFilterModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException)
            {
                bindingContext.ModelState.AddModelError(key: "filter", errorMessage: "Filter parameter is invalid JSON.");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
        }

        var result = new GetRoomsModel
        {
            Page = page,
            PageSize = pageSize,
            AfterRoomId = afterRoomId,
            Filter = filter
        };

        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;

        static int ParseIntOrDefault(string? value, int defaultValue)
        {
            return int.TryParse(value, out var v) ? v : defaultValue;
        }
    }
}