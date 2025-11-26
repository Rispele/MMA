using System.Net.Http.Json;
using System.Web;
using Rooms.Core.Clients.RoomSchedule;

namespace Rooms.Infrastructure.Clients.RoomSchedule;

public class RoomScheduleClient : IRoomScheduleClient
{
    private readonly HttpClient httpClient;

    public RoomScheduleClient(
        HttpClient httpClient,
        RoomScheduleClientSettings settings)
    {
        this.httpClient = httpClient;
        
        httpClient.BaseAddress = new Uri(settings.ApiGateway);
    }


    public IAsyncEnumerable<RoomScheduleResponseItemDto?> GetRoomSchedule(GetRoomScheduleRequest request, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder
        {
            Path = "events",
        };
        
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["date_gte"] = request.FromDate.ToString("yyyy-MM-dd");
        query["date_lt"] = request.ToDate.ToString("yyyy-MM-dd");
        query["address"] = request.ScheduleAddress.Address;
        query["room"] = request.ScheduleAddress.RoomNumber;
        uriBuilder.Query = query.ToString();

        var requestUri = uriBuilder.Uri.PathAndQuery;
        return httpClient.GetFromJsonAsAsyncEnumerable<RoomScheduleResponseItemDto>(requestUri , cancellationToken);
    }
}