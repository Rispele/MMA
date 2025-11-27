using Commons.Domain.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

internal sealed record FindEquipmentByIdQuery(int EquipmentId) : ISingleQuerySpecification<FindEquipmentByIdQuery, Domain.Models.Equipments.Equipment>;