using Commons.Core.Models.Filtering;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Domain.Propagated.Equipments;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Requests.Filtering;

namespace WebApi.Tests.UnitTests.Mappers.Helpers;

public static class EquipmentModelsMapperTestHelper
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

    private const int Page = 10;
    private const int PageSize = 10;

    private const int BatchNumber = 9;
    private const int BatchSize = 10;

    #region Equipments

    public static EquipmentModel CreateEquipmentModel()
    {
        return new EquipmentModel
        {
            Id = EquipmentId,
            RoomId = RoomId,
            SchemaId = SchemaId,
            Schema = CreateEquipmentSchemaModel(),
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
    }

    public static CreateEquipmentModel CreateCreateEquipmentModel()
    {
        return new CreateEquipmentModel
        {
            RoomId = RoomId,
            SchemaId = SchemaId,
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
    }

    public static GetEquipmentsModel CreateGetEquipmentsModel()
    {
        return new GetEquipmentsModel
        {
            AfterEquipmentId = EquipmentId,
            Page = Page,
            PageSize = PageSize,
            Filter = new EquipmentsFilterModel
            {
                Rooms = new FilterMultiParameterModel<int> { Values = [RoomId], SortDirection = SortDirectionModel.Ascending },
                Types = new FilterMultiParameterModel<int> { Values = [TypeId], SortDirection = SortDirectionModel.Descending },
                Schemas = new FilterMultiParameterModel<int> { Values = [SchemaId] },
                InventoryNumber = new FilterParameterModel<string> { Value = InventoryNumber },
                SerialNumber = new FilterParameterModel<string> { Value = SerialNumber },
                // NetworkEquipmentIp = new FilterParameterModel<string> { Value = NetworkEquipmentIp },
                // Comment = new FilterParameterModel<string> { Value = Comment },
                Statuses = new FilterMultiParameterModel<EquipmentStatus> { Values = [Status] }
            }
        };
    }

    public static PatchEquipmentModel CreatePatchEquipmentModel()
    {
        return new PatchEquipmentModel
        {
            RoomId = RoomId,
            SchemaId = SchemaId,
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
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

    public static CreateEquipmentDto CreateCreateEquipmentDto()
    {
        return new CreateEquipmentDto
        {
            RoomId = RoomId,
            SchemaId = SchemaId,
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
    }

    public static GetEquipmentsDto CreateGetEquipmentsDto()
    {
        return new GetEquipmentsDto(
            BatchNumber,
            BatchSize,
            EquipmentId,
            new EquipmentsFilterDto
            {
                Rooms = new FilterMultiParameterDto<int>([RoomId], SortDirectionDto.Ascending),
                Types = new FilterMultiParameterDto<int>([TypeId], SortDirectionDto.Descending),
                Schemas = new FilterMultiParameterDto<int>([SchemaId], SortDirectionDto.None),
                InventoryNumber = new FilterParameterDto<string>(InventoryNumber, SortDirectionDto.None),
                SerialNumber = new FilterParameterDto<string>(SerialNumber, SortDirectionDto.None),
                // NetworkEquipmentIp = new FilterParameterDto<string>(NetworkEquipmentIp, SortDirectionDto.None),
                // Comment = new FilterParameterDto<string>(Comment, SortDirectionDto.None),
                Statuses = new FilterMultiParameterDto<EquipmentStatus>([Status], SortDirectionDto.None)
            });
    }

    public static PatchEquipmentDto CreatePatchEquipmentDto()
    {
        return new PatchEquipmentDto
        {
            RoomId = RoomId,
            SchemaId = SchemaId,
            InventoryNumber = InventoryNumber,
            SerialNumber = SerialNumber,
            NetworkEquipmentIp = NetworkEquipmentIp,
            Comment = Comment,
            Status = Status
        };
    }

    #endregion

    #region Equipment Schema

    public static EquipmentSchemaModel CreateEquipmentSchemaModel()
    {
        return new EquipmentSchemaModel
        {
            Id = SchemaId,
            Name = SchemaName,
            TypeId = TypeId,
            Type = CreateEquipmentTypeModel(),
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
    }

    public static CreateEquipmentSchemaModel CreateCreateEquipmentSchemaModel()
    {
        return new CreateEquipmentSchemaModel
        {
            Name = SchemaName,
            EquipmentTypeId = TypeId,
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
    }

    public static GetEquipmentSchemasModel CreateGetEquipmentSchemasModel()
    {
        return new GetEquipmentSchemasModel
        {
            AfterEquipmentSchemaId = EquipmentId,
            Page = Page,
            PageSize = PageSize,
            Filter = new EquipmentSchemasFilterModel
            {
                Name = new FilterParameterModel<string> { Value = SchemaName },
                EquipmentTypeName = new FilterParameterModel<string> { Value = TypeName },
                EquipmentParameters = new FilterParameterModel<string> { Value = TypeParameterValue }
            }
        };
    }

    public static PatchEquipmentSchemaModel CreatePatchEquipmentSchemaModel()
    {
        return new PatchEquipmentSchemaModel
        {
            Name = SchemaName,
            EquipmentTypeId = TypeId,
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
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

    public static CreateEquipmentSchemaDto CreateCreateEquipmentSchemaDto()
    {
        return new CreateEquipmentSchemaDto
        {
            Name = SchemaName,
            EquipmentTypeId = TypeId,
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
    }

    public static GetEquipmentSchemasDto CreateGetEquipmentSchemasDto()
    {
        return new GetEquipmentSchemasDto(
            BatchNumber,
            BatchSize,
            EquipmentId,
            new EquipmentSchemasFilterDto
            {
                Name = new FilterParameterDto<string>(SchemaName, SortDirectionDto.None),
                EquipmentTypeName = new FilterParameterDto<string>(TypeName, SortDirectionDto.None),
                EquipmentParameters = new FilterParameterDto<string>(TypeParameterValue, SortDirectionDto.None)
            });
    }

    public static PatchEquipmentSchemaDto CreatePatchEquipmentSchemaDto()
    {
        return new PatchEquipmentSchemaDto
        {
            Name = SchemaName,
            EquipmentTypeId = TypeId,
            ParameterValues = new Dictionary<string, string>
            {
                [TypeParameterValue] = TypeParameterValue
            }
        };
    }

    #endregion

    #region Equipment Type

    public static EquipmentTypeModel CreateEquipmentTypeModel()
    {
        return new EquipmentTypeModel
        {
            Id = TypeId,
            Name = TypeName,
            Parameters =
            [
                new EquipmentParameterDescriptorModel
                {
                    Name = TypeParameterName,
                    Required = IsTypeParameterRequired
                }
            ]
        };
    }

    public static CreateEquipmentTypeModel CreateCreateEquipmentTypeModel()
    {
        return new CreateEquipmentTypeModel
        {
            Name = SchemaName,
            Parameters = [new EquipmentParameterDescriptorModel { Name = TypeParameterName, Required = IsTypeParameterRequired }]
        };
    }

    public static GetEquipmentTypesModel CreateGetEquipmentTypesModel()
    {
        return new GetEquipmentTypesModel
        {
            AfterEquipmentTypeId = TypeId,
            Page = Page,
            PageSize = PageSize,
            Filter = new EquipmentTypesFilterModel
            {
                Name = new FilterParameterModel<string> { Value = SchemaName }
            }
        };
    }

    public static PatchEquipmentTypeModel CreatePatchEquipmentTypeModel()
    {
        return new PatchEquipmentTypeModel
        {
            Name = TypeName,
            Parameters = [new EquipmentParameterDescriptorModel { Name = TypeParameterName, Required = IsTypeParameterRequired }]
        };
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

    public static CreateEquipmentTypeDto CreateCreateEquipmentTypeDto()
    {
        return new CreateEquipmentTypeDto
        {
            Name = SchemaName,
            Parameters = [new EquipmentParameterDescriptorDto { Name = TypeParameterName, Required = IsTypeParameterRequired }]
        };
    }

    public static GetEquipmentTypesDto CreateGetEquipmentTypesDto()
    {
        return new GetEquipmentTypesDto(
            BatchNumber,
            BatchSize,
            TypeId,
            new EquipmentTypesFilterDto
            {
                Name = new FilterParameterDto<string>(SchemaName, SortDirectionDto.None)
            });
    }

    public static PatchEquipmentTypeDto CreatePatchEquipmentTypeDto()
    {
        return new PatchEquipmentTypeDto
        {
            Name = TypeName,
            Parameters = [new EquipmentParameterDescriptorDto { Name = TypeParameterName, Required = IsTypeParameterRequired }]
        };
    }

    #endregion
}