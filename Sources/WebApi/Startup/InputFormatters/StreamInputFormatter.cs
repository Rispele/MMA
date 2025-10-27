using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace WebApi.Startup.InputFormatters;

public class StreamInputFormatter : InputFormatter
{
    public StreamInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(Stream);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(
        InputFormatterContext context)
    {
        var request = context.HttpContext.Request;
        return await InputFormatterResult.SuccessAsync(request.Body);
    }
}