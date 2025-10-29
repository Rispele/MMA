using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests.Equipments;

namespace WebApi.ModelBinders;

public class GetEquipmentsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], defaultValue: 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], defaultValue: 10);
        var afterEquipmentId = ParseIntOrDefault(q["afterEquipmentId"], defaultValue: 0);

        EquipmentsFilterModel? filter = null;
        if (q.TryGetValue(key: "filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<EquipmentsFilterModel>(json, new JsonSerializerOptions
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

        var result = new GetEquipmentsModel
        {
            Page = page,
            PageSize = pageSize,
            AfterEquipmentId = afterEquipmentId,
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