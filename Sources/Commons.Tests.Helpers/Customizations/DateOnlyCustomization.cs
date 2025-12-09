using AutoFixture;

namespace Commons.Tests.Helpers.Customizations;

public class DateOnlyCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime).OmitAutoProperties());
    }
}