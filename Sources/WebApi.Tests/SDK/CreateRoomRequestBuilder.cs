using Rooms.Core.Implementations.Dtos.Requests.Rooms;
using Rooms.Core.Implementations.Dtos.Room;

namespace WebApi.Tests.SDK;

public struct CreateRoomRequestBuilder
{
    private string? description;
    private RoomTypeDto type;
    private RoomLayoutDto layout;
    private int? seats;
    private int? computerSeats;
    private RoomNetTypeDto netType;
    private bool hasConditioning;
    private string? owner;
    private RoomStatusDto roomStatus;
    private string? comment;
    private DateTime? fixDeadline;
    private bool allowBooking;
    private readonly string name;

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

    public CreateRoomRequestBuilder Type(RoomTypeDto typeToSet)
    {
        type = typeToSet;
        return this;
    }

    public CreateRoomRequestBuilder Layout(RoomLayoutDto roomLayoutToSet)
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

    public CreateRoomRequestBuilder NetType(RoomNetTypeDto netTypeToSet)
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

    public CreateRoomRequestBuilder RoomStatus(RoomStatusDto roomStatusToSet)
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

    public CreateRoomRequest Build()
    {
        return new CreateRoomRequest
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

    public static implicit operator CreateRoomRequest(CreateRoomRequestBuilder builder)
    {
        return builder.Build();
    }
}