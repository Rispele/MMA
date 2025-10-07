using System.Net.Http.Json;
using Application.Clients.Dtos;
using Application.Clients.Dtos.Files;
using Application.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Clients.Implementations;

public class FileClient(HttpClient httpClient) : IFileClient
{
    public async Task<FileDto> GetFileById(Guid fileId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"files/{fileId}", cancellationToken);

        return await ProcessNonNullableResult<FileDto>(response, cancellationToken);
    }

    public async Task<FileLocationDto> StoreFile(byte[] content,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync("files", new ByteArrayContent(content), cancellationToken);

        return await ProcessNonNullableResult<FileLocationDto>(response, cancellationToken);
    }

    public async Task RemoveFileById(Guid fileId)
    {
        var response = await httpClient.DeleteAsync($"files/{fileId}");
    }

    private async Task<TResult> ProcessNonNullableResult<TResult>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await ProcessResult<TResult>(response, cancellationToken) ?? throw new InvalidOperationException("Response was found to be null");
    }

    private async Task<TResult?> ProcessResult<TResult>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);
        }

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

        throw new ApiException(problemDetails);
    }
}