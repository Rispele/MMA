using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.BookingRequests;
using WebApi.Core.Models.Requests.Equipments;
using WebApi.Core.Models.Requests.EquipmentSchemas;
using WebApi.Core.Models.Requests.EquipmentTypes;
using WebApi.Core.Models.Requests.InstituteResponsible;
using WebApi.Core.Models.Requests.OperatorDepartments;
using WebApi.Core.Models.Requests.Rooms;
using WebApi.ModelBinders;

namespace WebApi.Tests.UnitTests.ModelBinders;

[TestFixture]
public class GetRequestWithJsonFilterModelBinderTests
{
    private const int Page = 10, PageSize = 100, AfterId = 1000;

    [TestCaseSource(nameof(GetRequestWithJsonFilterModelBinderTestsCaseSource))]
    public async Task Test<TFilter>(GetRequestWithJsonFilterModelBinder<TFilter> binder, TFilter? filter)
        where TFilter : class
    {
        var expectedRequest = new GetRequest<TFilter>
        {
            Page = Page,
            PageSize = PageSize,
            AfterId = AfterId,
            Filter = filter
        };

        var mapped = await binder.BindModelInner(
            Page.ToString(),
            PageSize.ToString(),
            AfterId.ToString(),
            JsonSerializer.Serialize(filter),
            addModelError: (_, _) => { });

        mapped.IsModelSet.Should().BeTrue();
        mapped.Model.Should().BeEquivalentTo(expectedRequest);
    }

    private static IEnumerable<TestCaseData> GetRequestWithJsonFilterModelBinderTestsCaseSource()
    {
        var fixture = new Fixture();

        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<BookingRequestsFilterModel>(),
            fixture.Create<BookingRequestsFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<EquipmentsFilterModel>(),
            fixture.Create<EquipmentsFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<EquipmentSchemasFilterModel>(),
            fixture.Create<EquipmentSchemasFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<EquipmentTypesFilterModel>(),
            fixture.Create<EquipmentTypesFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<InstituteCoordinatorFilterModel>(),
            fixture.Create<InstituteCoordinatorFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<OperatorDepartmentsFilterModel>(),
            fixture.Create<OperatorDepartmentsFilterModel>());
        yield return new TestCaseData(
            new GetRequestWithJsonFilterModelBinder<RoomsFilterModel>(),
            fixture.Create<RoomsFilterModel>());
    }
}