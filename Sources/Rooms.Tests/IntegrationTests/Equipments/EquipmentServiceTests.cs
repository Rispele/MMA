using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;
using Rooms.Core.Services.Interfaces;

namespace Rooms.Tests.IntegrationTests.Equipments;

[TestFixture]
public class EquipmentServiceTests : ContainerTestBase
{
    [Inject]
    private readonly IEquipmentService equipmentService = null!;
    
    
}