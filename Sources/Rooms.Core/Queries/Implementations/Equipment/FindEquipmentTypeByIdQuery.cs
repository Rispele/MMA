using Commons.Domain.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

internal sealed record FindEquipmentTypeByIdQuery(int EquipmentTypeId) : ISingleQuerySpecification<FindEquipmentTypeByIdQuery, EquipmentType?>;