using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests;

namespace WebApi.ModelBinders;

public class GetEquipmentsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], 10);
        var afterRoomId = ParseIntOrDefault(q["afterRoomId"], 0);

        EquipmentsFilter? filter = null;
        if (q.TryGetValue("filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<EquipmentsFilter>(json, new JsonSerializerOptions
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

        var result = new EquipmentsRequest
        {
            Page = page,
            PageSize = pageSize,
            AfterEquipmentId = afterRoomId,
            Filter = filter
        };

        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;

        static int ParseIntOrDefault(string? value, int defaultValue)
            => int.TryParse(value, out var v) ? v : defaultValue;
    }
}