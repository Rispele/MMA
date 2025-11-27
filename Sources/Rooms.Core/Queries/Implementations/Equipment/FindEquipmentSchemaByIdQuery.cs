using Commons.Domain.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FindEquipmentSchemaByIdQuery(int EquipmentSchemaId) : ISingleQuerySpecification<FindEquipmentSchemaByIdQuery, EquipmentSchema>;