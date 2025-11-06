using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests.OperatorDepartments;

namespace WebApi.ModelBinders;

public class GetOperatorDepartmentsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], defaultValue: 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], defaultValue: 10);
        var afterOperatorDepartmentId = ParseIntOrDefault(q["afterOperatorDepartmentId"], defaultValue: 0);

        OperatorDepartmentsFilterModel? filter = null;
        if (q.TryGetValue(key: "filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<OperatorDepartmentsFilterModel>(json, new JsonSerializerOptions
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

        var result = new GetOperatorDepartmentsModel
        {
            Page = page,
            PageSize = pageSize,
            AfterOperatorDepartmentId = afterOperatorDepartmentId,
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