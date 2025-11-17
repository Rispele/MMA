using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FindEquipmentTypeByIdQuery(int EquipmentTypeId) : ISingleQuerySpecification<FindEquipmentTypeByIdQuery, EquipmentType>;