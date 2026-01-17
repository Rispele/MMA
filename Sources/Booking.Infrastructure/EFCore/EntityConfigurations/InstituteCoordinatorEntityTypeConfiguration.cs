using Booking.Domain.Models.InstituteCoordinators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class InstituteCoordinatorEntityTypeConfiguration : IEntityTypeConfiguration<InstituteCoordinator>
{
    public void Configure(EntityTypeBuilder<InstituteCoordinator> builder)
    {
        builder.HasKey(t => t.Id);
        builder
            .Property(t => t.Id)
            .HasField(InstituteCoordinatorFieldNames.Id)
            .ValueGeneratedOnAdd();

        builder.Ignore(t => t.Coordinators);
        builder.Property(InstituteCoordinatorFieldNames.Coordinators).HasColumnType("jsonb");

        builder.Property(t => t.InstituteId).IsRequired();
    }
}