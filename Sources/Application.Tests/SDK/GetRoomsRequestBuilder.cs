using Application.Clients.Dtos.Requests.RoomsQuerying;

namespace Application.Tests.SDK;

public struct GetRoomsRequestBuilder
{
    private readonly int batchNumber;
    private readonly int batchSize;
    private int afterRoomId = 0;
    private RoomsFilterDto? filter;

    private GetRoomsRequestBuilder(int batchNumber, int batchSize)
    {
        this.batchNumber = batchNumber;
        this.batchSize = batchSize;
    }

    public static GetRoomsRequestBuilder Create(int batchNumber, int batchSize) => new(batchNumber, batchSize);

    public GetRoomsRequestBuilder AfterRoomId(int roomId)
    {
        afterRoomId = roomId;
        return this;
    }

    public GetRoomsRequestBuilder Filter(Action<RoomsFilterBuilder> action)
    {
        var builder = new RoomsFilterBuilder();

        action(builder);
            
        filter = builder.Build();

        return this;
    }

    public GetRoomsRequest Build()
    {
        return new GetRoomsRequest(batchNumber, batchSize, afterRoomId, filter);
    }
    
    public static implicit operator GetRoomsRequest(GetRoomsRequestBuilder builder) => builder.Build();
}