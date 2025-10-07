using System.Net.Http.Json;
using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Dtos.Requests.RoomPatching;
using Application.Clients.Dtos.Requests.RoomsQuerying;
using Application.Clients.Dtos.Responses;
using Application.Clients.Dtos.Room;
using Application.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Clients.Implementations;

public class RoomsClient(HttpClient httpClient) : IRoomsClient
{
    public async Task<RoomDto> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"rooms/{roomId}", cancellationToken);

        return await ProcessNonNullableResult<RoomDto>(response, cancellationToken);
    }

    public async Task<RoomsResponseDto> GetRoomsAsync(GetRoomsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms/filter", request, cancellationToken);

        return await ProcessNonNullableResult<RoomsResponseDto>(response, cancellationToken);
    }

    public async Task<RoomDto> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms", request, cancellationToken);

        return await ProcessNonNullableResult<RoomDto>(response, cancellationToken);
    }

    public async Task PatchRoomAsync(int roomId, PatchRoomRequest patch, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"rooms?roomId={roomId}", patch, cancellationToken);

        await ProcessResult(response, cancellationToken);
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

    private async Task ProcessResult(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

        throw new ApiException(problemDetails);
    }
}