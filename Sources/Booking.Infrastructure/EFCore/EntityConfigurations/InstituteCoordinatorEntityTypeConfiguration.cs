using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.InstituteCoordinators;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.InstituteResponsibles;

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

        builder.Property(t => t.Institute).HasMaxLength(500).IsRequired();
    }
}