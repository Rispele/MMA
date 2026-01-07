using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.InstituteCoordinators;
using Booking.Infrastructure.EFCore.EntityConfigurations;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<BookingEvent> BookingEvents { get; [UsedImplicitly] private set; }

    public DbSet<BookingRequest> BookingRequests { get; [UsedImplicitly] private set; }

    public DbSet<InstituteCoordinator> InstituteCoordinators { get; [UsedImplicitly] private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingEventEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BookingRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InstituteCoordinatorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BookingProcessEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BookingEventRetryContextEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}