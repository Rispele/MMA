using System.Text.Json;
using Commons;
using Microsoft.AspNetCore.Mvc;

namespace Application.Clients;

public class ApiException(ProblemDetails? problemDetails) : Exception(FormatErrorMessage(problemDetails))
{
    private static string FormatErrorMessage(ProblemDetails? problemDetails)
    {
        if (problemDetails is null)
        {
            return "Error response received. Details: No Details Provided";
        }

        var extensions = problemDetails.Extensions
            .Select(t => (t.Key, Value: t.Value as JsonElement?))
            .Select(t => $"{t.Key}: {t.Value}")
            .JoinStrings(";\n    ");

        return $"""
                Error response received. Information:
                  Status: {problemDetails.Status}
                  Title: {problemDetails.Title}
                  Detail: {problemDetails.Detail ?? "Not Detail Provided"}
                  Extensions: 
                    {extensions}
                """;
    }
}