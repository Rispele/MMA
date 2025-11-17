using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FindEquipmentByIdQuery(int EquipmentId) : ISingleQuerySpecification<FindEquipmentByIdQuery, Domain.Models.Equipments.Equipment>;