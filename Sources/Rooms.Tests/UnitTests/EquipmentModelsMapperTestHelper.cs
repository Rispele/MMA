using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Propagated.Equipments;

namespace Rooms.Tests.UnitTests;

public static class EquipmentsTestHelper
{
    private const int EquipmentId = 1;
    private const int RoomId = 2;
    private const int SchemaId = 3;
    private const string InventoryNumber = nameof(InventoryNumber);
    private const string SerialNumber = nameof(SerialNumber);
    private const string NetworkEquipmentIp = nameof(NetworkEquipmentIp);
    private const string Comment = nameof(Comment);
    private const EquipmentStatus Status = EquipmentStatus.Error;

    private const string SchemaName = nameof(SchemaName);
    private const int TypeId = 4;

    private const string TypeName = nameof(TypeName);
    private const string TypeParameterName = nameof(TypeParameterName);
    private const string TypeParameterValue = nameof(TypeParameterValue);
    private const bool IsTypeParameterRequired = true;

    #region Equipments

    public static Equipment CreateEquipment()
    {
        return new Equipment(
            EquipmentId,
            RoomId,
            CreateEquipmentSchema(),
            InventoryNumber,
            SerialNumber,
            NetworkEquipmentIp,
            Comment,
            Status);
    }


    public static EquipmentDto CreateEquipmentDto()
    {
        return new EquipmentDto
        {
            Id = EquipmentId,
            RoomId = RoomId,
            Schema = CreateEquipmentSchemaDto(),
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
    }

    #endregion

    #region Equipment Schema

    public static EquipmentSchema CreateEquipmentSchema()
    {
        return new EquipmentSchema(
            SchemaId,
            SchemaName,
            CreateEquipmentType(),
            new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            });
    }

    public static EquipmentSchemaDto CreateEquipmentSchemaDto()
    {
        return new EquipmentSchemaDto
        {
            Id = SchemaId,
            Name = SchemaName,
            Type = CreateEquipmentTypeDto(),
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
    }

    #endregion

    #region Equipment Type

    public static EquipmentType CreateEquipmentType()
    {
        return new EquipmentType(
            TypeId,
            TypeName,
            [
                new EquipmentParameterDescriptor(TypeParameterName, IsTypeParameterRequired)
            ]);
    }

    public static EquipmentTypeDto CreateEquipmentTypeDto()
    {
        return new EquipmentTypeDto
        {
            Id = TypeId,
            Name = TypeName,
            Parameters = [new EquipmentParameterDescriptorDto { Name = TypeParameterName, Required = IsTypeParameterRequired }]
        };
    }

    #endregion
}