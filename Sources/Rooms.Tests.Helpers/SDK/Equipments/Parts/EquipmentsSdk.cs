using JetBrains.Annotations;
using Rooms.Core.Services.Equipments.Interfaces;

namespace Rooms.Tests.Helpers.SDK.Equipments.Parts;

[UsedImplicitly]
public partial class EquipmentsSdk(
    IEquipmentService equipmentService,
    IEquipmentSchemaService equipmentSchemaService,
    IEquipmentTypeService equipmentTypeService);