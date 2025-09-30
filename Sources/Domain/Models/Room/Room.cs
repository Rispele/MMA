using Domain.Models.Room.Fix;
using Domain.Models.Room.Parameters;

namespace Domain.Models.Room;

public class Room
{
    private readonly int id;
    private readonly string name;
    private readonly string description;
    private readonly RoomParameters parameters;
    private string owner;
    private readonly RoomFixInfo fixInfo;
    private bool allowBooking;
    
    public Room(
        int id,
        string name,
        string description,
        RoomParameters parameters, 
        string owner,
        RoomFixInfo fixInfo,
        bool allowBooking)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.parameters = parameters;
        this.fixInfo = fixInfo;
        this.owner = owner;
        this.allowBooking = allowBooking;
    }
}