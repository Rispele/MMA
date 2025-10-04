using System.Net.Http.Json;
using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Dtos.Requests.RoomPatching;
using Application.Clients.Dtos.Requests.RoomsQuerying;
using Application.Clients.Dtos.Responses;
using Application.Clients.Dtos.Room;

namespace Application.Clients;

public class RoomsClient(HttpClient httpClient) : IRoomsClient
{
    public async Task<RoomDto?> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<RoomDto>($"rooms/{roomId}", cancellationToken);
    }

    public async Task<RoomsResponseDto?> GetRoomsAsync(GetRoomsRequestDto request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms/filter", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<RoomsResponseDto>(cancellationToken: cancellationToken);
    }

    public async Task<RoomDto?> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("rooms", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<RoomDto>(cancellationToken: cancellationToken);
    }

    public async Task PatchRoomAsync(int roomId, PatchRoomRequest patch, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PatchAsJsonAsync($"rooms?roomId={roomId}", patch, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}