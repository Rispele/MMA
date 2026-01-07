using AutoFixture;
using AutoFixture.Kernel;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Commons.Tests.Helpers.Customizations;

namespace Booking.Tests.UnitTests;

public static class FixtureExtensions
{
    extension(IFixture fixture)
    {
        public IFixture WithBookingRequestCustomization()
        {
            return fixture
                .WithDateOnlyCustomization()
                .WithCoordinatorDtoCustomization(typeof(ScientificRoomEventCoordinatorDto))
                .WithCoordinatorCustomization(typeof(ScientificRoomEventCoordinator));
        }

        public IFixture WithCoordinatorDtoCustomization(Type coordinatorType)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IRoomEventCoordinatorDto), coordinatorType));

            return fixture;
        }

        public IFixture WithCoordinatorCustomization(Type coordinatorType)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IRoomEventCoordinator), coordinatorType));

            return fixture;
        }
    }
}