using Application.Clients.Dtos.Requests.RoomPatching;
using Application.Clients.Dtos.Room;

namespace Application.Tests.SDK;

public class PatchRoomRequestBuilder
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

    private PatchRoomRequestBuilder(string name)
    {
        this.name = name;
    }

    public static PatchRoomRequestBuilder Create(string name)
    {
        return new PatchRoomRequestBuilder(name);
    }

    public PatchRoomRequestBuilder Description(string descriptionToSet)
    {
        description = descriptionToSet;
        return this;
    }

    public PatchRoomRequestBuilder Type(RoomTypeDto typeToSet)
    {
        type = typeToSet;
        return this;
    }

    public PatchRoomRequestBuilder Layout(RoomLayoutDto roomLayoutToSet)
    {
        layout = roomLayoutToSet;
        return this;
    }

    public PatchRoomRequestBuilder Seats(int seatsToSet)
    {
        seats = seatsToSet;
        return this;
    }

    public PatchRoomRequestBuilder ComputerSeats(int computerSeatsToSet)
    {
        computerSeats = computerSeatsToSet;
        return this;
    }

    public PatchRoomRequestBuilder NetType(RoomNetTypeDto netTypeToSet)
    {
        netType = netTypeToSet;
        return this;
    }

    public PatchRoomRequestBuilder HasConditioning(bool hasConditioningToSet = true)
    {
        hasConditioning = hasConditioningToSet;
        return this;
    }

    public PatchRoomRequestBuilder Owner(string ownerToSet)
    {
        owner = ownerToSet;
        return this;
    }

    public PatchRoomRequestBuilder RoomStatus(RoomStatusDto roomStatusToSet)
    {
        roomStatus = roomStatusToSet;
        return this;
    }

    public PatchRoomRequestBuilder Comment(string commentToSet)
    {
        comment = commentToSet;
        return this;
    }

    public PatchRoomRequestBuilder FixDeadline(DateTime? fixDeadlineToSet)
    {
        fixDeadline = fixDeadlineToSet;
        return this;
    }

    public PatchRoomRequestBuilder AllowBooking(bool allowBookingToSet = true)
    {
        allowBooking = allowBookingToSet;
        return this;
    }

    public PatchRoomRequest Build()
    {
        return new PatchRoomRequest
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

    public static implicit operator PatchRoomRequest(PatchRoomRequestBuilder builder)
    {
        return builder.Build();
    }
}