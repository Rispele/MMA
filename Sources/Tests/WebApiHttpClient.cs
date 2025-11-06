using System.Net.Http.Json;
using WebApi.Models.Equipment;
using WebApi.Models.OperatorDepartments;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Requests.OperatorDepartments;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace Tests;

public class WebApiHttpClient(HttpClient httpClient, string baseUrl)
{
    #region EquipmentTypesController

    public async Task<EquipmentTypeModel> CreateEquipmentType(CreateEquipmentTypeModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/webapi/equipment-types", model);
        return (await response.Content.ReadFromJsonAsync<EquipmentTypeModel>())!;
    }

    public async Task<EquipmentTypeModel> GetEquipmentTypeById(int equipmentTypeId)
    {
        return (await httpClient.GetFromJsonAsync($"{baseUrl}/webapi/equipment-types/{equipmentTypeId}", typeof(EquipmentTypeModel)) as
            EquipmentTypeModel)!;
    }

    #endregion

    #region EquipmentSchemaController

    public async Task<EquipmentSchemaModel> CreateEquipmentSchema(CreateEquipmentSchemaModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/webapi/equipment-schemas", model);
        return (await response.Content.ReadFromJsonAsync<EquipmentSchemaModel>())!;
    }

    public async Task<EquipmentSchemaModel> GetEquipmentSchemaById(int equipmentSchemaId)
    {
        return (await httpClient.GetFromJsonAsync($"{baseUrl}/webapi/equipment-schemas/{equipmentSchemaId}", typeof(EquipmentSchemaModel)) as
            EquipmentSchemaModel)!;
    }

    #endregion

    #region EquipmentController

    public async Task<EquipmentModel> CreateEquipment(CreateEquipmentModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/webapi/equipments", model);
        return (await response.Content.ReadFromJsonAsync<EquipmentModel>())!;
    }

    #endregion

    #region RoomsController

    public async Task<RoomModel> CreateRoom(CreateRoomModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/webapi/rooms", model);
        return (await response.Content.ReadFromJsonAsync<RoomModel>())!;
    }

    public async Task<RoomModel> GetRoomById(int roomId)
    {
        return (await httpClient.GetFromJsonAsync($"{baseUrl}/webapi/rooms/{roomId}", typeof(RoomModel)) as RoomModel)!;
    }

    #endregion

    #region OperatorDepartmentsController

    public async Task<OperatorDepartmentModel> CreateOperatorDepartment(CreateOperatorDepartmentModel model)
    {
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/webapi/operator-rooms", model);
        return (await response.Content.ReadFromJsonAsync<OperatorDepartmentModel>())!;
    }

    public async Task<OperatorDepartmentModel> GetOperatorDepartmentById(int operatorDepartmentId)
    {
        return (await httpClient.GetFromJsonAsync($"{baseUrl}/webapi/operator-rooms/{operatorDepartmentId}", typeof(OperatorDepartmentModel)) as OperatorDepartmentModel)!;
    }

    #endregion
}