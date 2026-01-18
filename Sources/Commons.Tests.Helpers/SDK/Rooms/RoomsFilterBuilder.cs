using Commons.Core.Models.Filtering;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Domain.Propagated.Rooms;

namespace Commons.Tests.Helpers.SDK.Rooms;

public class RoomsFilterBuilder
{
    private FilterParameterDto<string>? comment;
    private FilterParameterDto<int>? computerSeats;
    private FilterParameterDto<string>? description;
    private FilterParameterDto<DateTime>? fixDeadline;
    private FilterParameterDto<bool>? hasConditioning;
    private FilterMultiParameterDto<RoomLayout>? layout;
    private FilterParameterDto<string>? name;
    private FilterMultiParameterDto<RoomNetType>? netTypes;
    private FilterParameterDto<string>? owner;
    private FilterParameterDto<int>? seats;
    private FilterMultiParameterDto<RoomStatus>? status;
    private FilterMultiParameterDto<RoomType>? types;

    public RoomsFilterBuilder Name(string value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        name = new FilterParameterDto<string>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Description(string value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        description = new FilterParameterDto<string>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Type(RoomType[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        types = new FilterMultiParameterDto<RoomType>(values, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Layout(RoomLayout[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        layout = new FilterMultiParameterDto<RoomLayout>(values, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Seats(int value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        seats = new FilterParameterDto<int>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder ComputerSeats(int value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        computerSeats = new FilterParameterDto<int>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder NetType(RoomNetType[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        netTypes = new FilterMultiParameterDto<RoomNetType>(values, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Conditioning(bool value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        hasConditioning = new FilterParameterDto<bool>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Owner(string value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        owner = new FilterParameterDto<string>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder RoomStatus(RoomStatus[] value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        status = new FilterMultiParameterDto<RoomStatus>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder FixDeadline(DateTime value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        fixDeadline = new FilterParameterDto<DateTime>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Comment(string value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        comment = new FilterParameterDto<string>(value, sortDirection);
        return this;
    }

    public RoomsFilterDto Build()
    {
        return new RoomsFilterDto
        {
            Name = name,
            Description = description,
            RoomTypes = types,
            RoomLayout = layout,
            Seats = seats,
            ComputerSeats = computerSeats,
            NetTypes = netTypes,
            Conditioning = hasConditioning,
            Owner = owner,
            RoomStatuses = status,
            FixDeadline = fixDeadline,
            Comment = comment
        };
    }

    public static implicit operator RoomsFilterDto(RoomsFilterBuilder builder)
    {
        return builder.Build();
    }
}