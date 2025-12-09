using AutoFixture;
using AutoFixture.Kernel;
using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Commons.Tests.Helpers.Customizations;
using WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

namespace WebApi.Tests.UnitTests;

public static class FixtureExtensions
{
    extension(IFixture fixture)
    {
        public IFixture WithBookingRequestCustomization()
        {
            return fixture
                .WithDateOnlyCustomization()
                .WithCoordinatorModelCustomization(typeof(ScientificRoomEventCoordinatorModel))
                .WithCoordinatorDtoCustomization(typeof(ScientificRoomEventCoordinatorDto));
        }

        public IFixture WithCoordinatorModelCustomization(Type coordinatorType)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IRoomEventCoordinatorModel), coordinatorType));

            return fixture;
        }

        public IFixture WithCoordinatorDtoCustomization(Type coordinatorType)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IRoomEventCoordinatorDto), coordinatorType));

            return fixture;
        }
    }
}