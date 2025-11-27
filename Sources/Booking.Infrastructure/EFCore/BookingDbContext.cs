using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.InstituteCoordinators;
using Booking.Infrastructure.EFCore.EntityConfigurations;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore;

public class BookingDbContext : DbContext
{
    public DbSet<BookingRequest> BookingRequests { get; [UsedImplicitly] private set; }

    public DbSet<InstituteCoordinator> InstituteCoordinators { get; [UsedImplicitly] private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InstituteCoordinatorEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}