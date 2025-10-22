namespace Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;

public record GetEquipmentsRequestDto
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; }
    public EquipmentsFilterDto? Filter { get; init; }
}