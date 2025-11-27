using Commons.Domain.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

internal sealed record FindEquipmentSchemaByIdQuery(int EquipmentSchemaId) : ISingleQuerySpecification<FindEquipmentSchemaByIdQuery, EquipmentSchema>;