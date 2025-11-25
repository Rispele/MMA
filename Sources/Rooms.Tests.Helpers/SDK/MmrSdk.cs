using Rooms.Tests.Helpers.SDK.Equipments.Parts;
using Rooms.Tests.Helpers.SDK.Rooms;

namespace Rooms.Tests.Helpers.SDK;

public record MmrSdk(
    RoomsSdk Rooms,
    EquipmentsSdk Equipments);
