using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Models.Requests.BookingRequests;

namespace WebApi.ModelBinders;

public class GetBookingRequestsRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var q = bindingContext.HttpContext.Request.Query;

        // read basic scalars
        var page = ParseIntOrDefault(q["page"], defaultValue: 1);
        var pageSize = ParseIntOrDefault(q["pageSize"], defaultValue: 10);
        var afterBookingRequestId = ParseIntOrDefault(q["afterBookingRequestId"], defaultValue: 0);

        BookingRequestsFilterModel? filter = null;
        if (q.TryGetValue(key: "filter", out var filterValues) && filterValues.Count > 0)
        {
            var json = filterValues[0];
            try
            {
                filter = JsonSerializer.Deserialize<BookingRequestsFilterModel>(json, new JsonSerializerOptions
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

        var result = new GetBookingRequestsModel
        {
            Page = page,
            PageSize = pageSize,
            AfterBookingRequestId = afterBookingRequestId,
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