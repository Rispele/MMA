using AutoFixture;

namespace Commons.Tests.Helpers.Customizations;

public static class CustomizationExtensions
{
    public static IFixture WithDateOnlyCustomization(this IFixture fixture)
    {
        return fixture.Customize(new DateOnlyCustomization());
    }
}