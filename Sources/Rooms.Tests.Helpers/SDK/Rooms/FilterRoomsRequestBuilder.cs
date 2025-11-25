using Rooms.Core.Dtos.Requests.Rooms;

namespace Rooms.Tests.Helpers.SDK.Rooms;

public class FilterRoomsRequestBuilder
{
    private readonly int batchNumber;
    private readonly int batchSize;
    private int afterRoomId;
    private RoomsFilterDto? filter;

    private FilterRoomsRequestBuilder(int batchNumber, int batchSize)
    {
        this.batchNumber = batchNumber;
        this.batchSize = batchSize;
    }

    public static FilterRoomsRequestBuilder Create(int batchNumber, int batchSize)
    {
        return new FilterRoomsRequestBuilder(batchNumber, batchSize);
    }

    public FilterRoomsRequestBuilder AfterRoomId(int roomId)
    {
        afterRoomId = roomId;
        return this;
    }

    public FilterRoomsRequestBuilder Filter(Action<RoomsFilterBuilder> action)
    {
        var builder = new RoomsFilterBuilder();

        action(builder);

        filter = builder.Build();

        return this;
    }

    public GetRoomsRequestDto Build()
    {
        return new GetRoomsRequestDto
        {
            BatchNumber = batchNumber,
            BatchSize = batchSize,
            AfterRoomId = afterRoomId,
            Filter = filter
        };
    }

    public static implicit operator GetRoomsRequestDto(FilterRoomsRequestBuilder builder)
    {
        return builder.Build();
    }
}