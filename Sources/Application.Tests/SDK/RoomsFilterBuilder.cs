using Application.Clients.Dtos.Requests.Filtering;
using Application.Clients.Dtos.Requests.RoomsQuerying;
using Application.Clients.Dtos.Room;

namespace Application.Tests.SDK;

public class RoomsFilterBuilder
{
    private FilterParameterDto<string>? name;
    private FilterParameterDto<string>? description;
    private FilterMultiParameterDto<RoomTypeDto>? types;
    private FilterMultiParameterDto<RoomLayoutDto>? layout;
    private FilterParameterDto<int>? seats;
    private FilterParameterDto<int>? computerSeats;
    private FilterMultiParameterDto<RoomNetTypeDto>? netTypes;
    private FilterParameterDto<bool>? hasConditioning;
    private FilterParameterDto<string>? owner;
    private FilterMultiParameterDto<RoomStatusDto>? status;
    private FilterParameterDto<DateTime>? fixDeadline;
    private FilterParameterDto<string>? comment;
    private FilterParameterDto<bool>? allowBooking;

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

    public RoomsFilterBuilder Type(RoomTypeDto[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        types = new FilterMultiParameterDto<RoomTypeDto>(values, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Layout(RoomLayoutDto[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        layout = new FilterMultiParameterDto<RoomLayoutDto>(values, sortDirection);
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

    public RoomsFilterBuilder NetType(RoomNetTypeDto[] values, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        netTypes = new FilterMultiParameterDto<RoomNetTypeDto>(values, sortDirection);
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

    public RoomsFilterBuilder RoomStatus(RoomStatusDto[] value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        status = new FilterMultiParameterDto<RoomStatusDto>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder FixDeadline(DateTime value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        fixDeadline =  new FilterParameterDto<DateTime>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder Comment(string value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        comment =  new FilterParameterDto<string>(value, sortDirection);
        return this;
    }

    public RoomsFilterBuilder AllowBooking(bool value, SortDirectionDto sortDirection = SortDirectionDto.None)
    {
        allowBooking =  new FilterParameterDto<bool>(value, sortDirection);
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
            Comment = comment,
            AllowBooking = allowBooking
        };
    }

    public static implicit operator RoomsFilterDto(RoomsFilterBuilder builder)
    {
        return builder.Build();
    }
}