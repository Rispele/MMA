using System.Net.Http.Json;
using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Dtos.Requests.RoomPatching;
using Application.Clients.Dtos.Requests.RoomsQuerying;
using Application.Clients.Dtos.Responses;
using Application.Clients.Dtos.Room;
using Microsoft.AspNetCore.Mvc;

namespace Application.Clients;

public class RoomsClient(HttpClient httpClient) : IRoomsClient
{
    public async Task<RoomDto> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default)
    {
        var room = await httpClient.GetFromJsonAsync<RoomDto>($"rooms/{roomId}", cancellationToken);

        return room!;
    }

    public async Task<RoomsResponseDto> GetRoomsAsync(GetRoomsRequestDto request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms/filter", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var roomsResponse = await response.Content.ReadFromJsonAsync<RoomsResponseDto>(cancellationToken: cancellationToken);

        return roomsResponse!;
    }

    public async Task<RoomDto> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms", request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return (await response.Content.ReadFromJsonAsync<RoomDto>(cancellationToken: cancellationToken))!;
        }

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

        throw new ApiException(problemDetails);
    }

    public async Task PatchRoomAsync(int roomId, PatchRoomRequest patch, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"rooms?roomId={roomId}", patch, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}