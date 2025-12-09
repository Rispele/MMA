using Commons.Tests.Helpers.SDK.Equipments.Parts;
using Commons.Tests.Helpers.SDK.Rooms;

namespace Commons.Tests.Helpers.SDK;

public record MmrSdk(
    RoomsSdk Rooms,
    EquipmentsSdk Equipments);
