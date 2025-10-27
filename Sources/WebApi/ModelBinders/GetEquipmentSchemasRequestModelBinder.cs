using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests.EquipmentSchemas;

namespace WebApi.ModelBinders;

public class GetEquipmentSchemasRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], 10);
        var afterEquipmentSchemaId = ParseIntOrDefault(q["afterEquipmentSchemaId"], 0);

        EquipmentSchemasFilterModel? filter = null;
        if (q.TryGetValue("filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<EquipmentSchemasFilterModel>(json, new JsonSerializerOptions
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

        var result = new GetEquipmentSchemasModel()
        {
            Page = page,
            PageSize = pageSize,
            AfterEquipmentSchemaId = afterEquipmentSchemaId,
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