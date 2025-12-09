using Commons.Core.Models.Filtering;
using FluentAssertions;
using WebApi.Models.Requests.Filtering;

namespace WebApi.Tests.UnitTests.Mappers.Helpers;

public static class AssertionHelper
{
    public static void AssertNullablesEqual<TActual, TExpected>(
        TActual? actual,
        TExpected? expected,
        Action<TActual, TExpected> assertion)
    {
        if (expected is null)
        {
            actual.Should().BeNull();
        }
        else
        {
            actual.Should().NotBeNull();

            assertion(actual, expected);
        }
    }

    public static void AssertFilterParameter<TActual, TExpected>(
        FilterMultiParameterDto<TActual>? actual,
        FilterMultiParameterModel<TExpected>? expected)
    {
        if (expected is null)
        {
            actual.Should().BeNull();
        }
        else
        {
            actual.Should().NotBeNull();

            var mappedDirection = actual.SortDirection;
            var initialDirection = expected.SortDirection;
            switch (initialDirection)
            {
                case SortDirectionModel.None:
                    mappedDirection.Should().Be(SortDirectionDto.None);
                    break;
                case SortDirectionModel.Ascending:
                    mappedDirection.Should().Be(SortDirectionDto.Ascending);
                    break;
                case SortDirectionModel.Descending:
                    mappedDirection.Should().Be(SortDirectionDto.Descending);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var mappedValues = actual.Values;
            var initialValues = expected.Values;

            mappedValues.Should().BeEquivalentTo(initialValues);
        }
    }
    
    public static void AssertFilterParameter<TActual, TExpected>(
        FilterParameterDto<TActual>? actual,
        FilterParameterModel<TExpected>? expected)
    {
        if (expected is null)
        {
            actual.Should().BeNull();
        }
        else
        {
            actual.Should().NotBeNull();

            var mappedDirection = actual.SortDirection;
            var initialDirection = expected.SortDirection;
            switch (initialDirection)
            {
                case SortDirectionModel.None:
                    mappedDirection.Should().Be(SortDirectionDto.None);
                    break;
                case SortDirectionModel.Ascending:
                    mappedDirection.Should().Be(SortDirectionDto.Ascending);
                    break;
                case SortDirectionModel.Descending:
                    mappedDirection.Should().Be(SortDirectionDto.Descending);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            actual.Value.Should().BeEquivalentTo(expected.Value);
        }
    }
}