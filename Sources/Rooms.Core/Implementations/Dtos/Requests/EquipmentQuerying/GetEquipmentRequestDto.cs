namespace Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;

public record GetEquipmentRequestDto
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; }
    public EquipmentsFilterDto? Filter { get; init; }
}