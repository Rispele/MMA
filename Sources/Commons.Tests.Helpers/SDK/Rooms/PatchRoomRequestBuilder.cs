using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Domain.Propagated.Rooms;

namespace Commons.Tests.Helpers.SDK.Rooms;

public class PatchRoomRequestBuilder
{
    private bool allowBooking;
    private string? comment;
    private int? computerSeats;
    private string? description;
    private DateTime? fixDeadline;
    private bool? hasConditioning;
    private RoomLayout layout;
    private string name;
    private RoomNetType netType;
    private string? owner;
    private RoomStatus roomStatus;
    private int? seats;
    private RoomType type;

    private PatchRoomRequestBuilder(
        string name,
        string? description,
        RoomType type,
        RoomLayout layout,
        int? seats,
        int? computerSeats,
        RoomNetType netType,
        bool? hasConditioning,
        string? owner,
        RoomStatus roomStatus,
        string? comment,
        DateTime? fixDeadline,
        bool allowBooking)
    {
        this.name = name;
        this.description = description;
        this.type = type;
        this.layout = layout;
        this.seats = seats;
        this.computerSeats = computerSeats;
        this.netType = netType;
        this.hasConditioning = hasConditioning;
        this.owner = owner;
        this.roomStatus = roomStatus;
        this.comment = comment;
        this.fixDeadline = fixDeadline;
        this.allowBooking = allowBooking;
    }

    public static PatchRoomRequestBuilder Create(RoomDto room)
    {
        return new PatchRoomRequestBuilder(
            room.Name,
            room.Description,
            room.Parameters.Type,
            room.Parameters.Layout,
            room.Parameters.Seats,
            room.Parameters.ComputerSeats,
            room.Parameters.NetType,
            room.Parameters.HasConditioning,
            room.Owner,
            room.FixInfo.Status,
            room.FixInfo.Comment,
            room.FixInfo.FixDeadline,
            room.AllowBooking);
    }

    public PatchRoomRequestBuilder Name(string nameToSet)
    {
        name = nameToSet;
        return this;
    }

    public PatchRoomRequestBuilder Description(string descriptionToSet)
    {
        description = descriptionToSet;
        return this;
    }

    public PatchRoomRequestBuilder Type(RoomType typeToSet)
    {
        type = typeToSet;
        return this;
    }

    public PatchRoomRequestBuilder Layout(RoomLayout roomLayoutToSet)
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

    public PatchRoomRequestBuilder NetType(RoomNetType netTypeToSet)
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

    public PatchRoomRequestBuilder RoomStatus(RoomStatus roomStatusToSet)
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

    public PatchRoomDto Build()
    {
        return new PatchRoomDto
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

    public static implicit operator PatchRoomDto(PatchRoomRequestBuilder builder)
    {
        return builder.Build();
    }
}