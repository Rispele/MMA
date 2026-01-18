using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Domain.Propagated.Rooms;

namespace Commons.Tests.Helpers.SDK.Rooms;

public class CreateRoomRequestBuilder
{
    private readonly string name;
    private bool allowBooking;
    private string? comment;
    private int? computerSeats;
    private string? description;
    private DateTime? fixDeadline;
    private bool hasConditioning;
    private RoomLayout layout;
    private RoomNetType netType;
    private string? owner;
    private RoomStatus roomStatus;
    private int? seats;
    private RoomType type;

    private CreateRoomRequestBuilder(string name)
    {
        this.name = name;
    }

    public static CreateRoomRequestBuilder Create(string name)
    {
        return new CreateRoomRequestBuilder(name);
    }

    public CreateRoomRequestBuilder Description(string descriptionToSet)
    {
        description = descriptionToSet;
        return this;
    }

    public CreateRoomRequestBuilder Type(RoomType typeToSet)
    {
        type = typeToSet;
        return this;
    }

    public CreateRoomRequestBuilder Layout(RoomLayout roomLayoutToSet)
    {
        layout = roomLayoutToSet;
        return this;
    }

    public CreateRoomRequestBuilder Seats(int seatsToSet)
    {
        seats = seatsToSet;
        return this;
    }

    public CreateRoomRequestBuilder ComputerSeats(int computerSeatsToSet)
    {
        computerSeats = computerSeatsToSet;
        return this;
    }

    public CreateRoomRequestBuilder NetType(RoomNetType netTypeToSet)
    {
        netType = netTypeToSet;
        return this;
    }

    public CreateRoomRequestBuilder HasConditioning(bool hasConditioningToSet = true)
    {
        hasConditioning = hasConditioningToSet;
        return this;
    }

    public CreateRoomRequestBuilder Owner(string ownerToSet)
    {
        owner = ownerToSet;
        return this;
    }

    public CreateRoomRequestBuilder RoomStatus(RoomStatus roomStatusToSet)
    {
        roomStatus = roomStatusToSet;
        return this;
    }

    public CreateRoomRequestBuilder Comment(string commentToSet)
    {
        comment = commentToSet;
        return this;
    }

    public CreateRoomRequestBuilder FixDeadline(DateTime? fixDeadlineToSet)
    {
        fixDeadline = fixDeadlineToSet;
        return this;
    }

    public CreateRoomRequestBuilder AllowBooking(bool allowBookingToSet = true)
    {
        allowBooking = allowBookingToSet;
        return this;
    }

    public CreateRoomDto Build()
    {
        return new CreateRoomDto
        {
            Name = name,
            Description = description,
            Type = type,
            Layout = layout,
            Seats = seats,
            ComputerSeats = computerSeats,
            NetType = netType,
            HasConditioning = hasConditioning,
            Owner = owner,
            RoomStatus = roomStatus,
            Comment = comment,
            FixDeadline = fixDeadline,
            AllowBooking = allowBooking
        };
    }

    public static implicit operator CreateRoomDto(CreateRoomRequestBuilder builder)
    {
        return builder.Build();
    }
}