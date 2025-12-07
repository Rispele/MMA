using Sources.AppHost.Resources.Specifications;

namespace Sources.AppHost.Resources;

public static class KnownResources
{
    public static readonly ResourceSpecification WebApiService = new(
        Name: "webapi",
        [
            new EndpointSpecification(KnownEndpoints.Http, TargetPort: 8888),
            new EndpointSpecification(KnownEndpoints.Https, TargetPort: 8889)
        ]);

    public static readonly ResourceSpecification RoomsMigrationService = new("roomsMigration");
    public static readonly ResourceSpecification BookingsMigrationService = new("bookingsMigration");

    public static readonly ResourceSpecification PostgresService =
        new(Name: "postgress", [new EndpointSpecification(KnownEndpoints.Http, TargetPort: 5432)]);

    public static readonly ResourceSpecification MmrDb = new("mmr");

    #region Custom

    public static readonly ResourceSpecification Minio = new(
        Name: "minio",
        [
            new EndpointSpecification(KnownEndpoints.Http, TargetPort: 9000),
            new EndpointSpecification(KnownEndpoints.Admin, TargetPort: 9001)
        ]);

    public static readonly ResourceSpecification TestDoubleLkUserApi = new(
        Name: "testdoublelkuserapi",
        [
            new EndpointSpecification(KnownEndpoints.Http, TargetPort: 3413)
        ]);

    #endregion
}