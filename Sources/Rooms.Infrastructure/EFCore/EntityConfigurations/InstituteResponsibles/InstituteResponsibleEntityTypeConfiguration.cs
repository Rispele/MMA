using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.InstituteResponsibles;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.InstituteResponsibles;

public class InstituteResponsibleEntityTypeConfiguration : IEntityTypeConfiguration<InstituteResponsible>
{
    public void Configure(EntityTypeBuilder<InstituteResponsible> builder)
    {
        builder.HasKey(t => t.Id);
        builder
            .Property(t => t.Id)
            .HasField(InstituteResponsibleFieldNames.Id)
            .ValueGeneratedOnAdd();

        builder.Ignore(t => t.Responsible);
        builder.Property(InstituteResponsibleFieldNames.Responsible).HasColumnType("jsonb");

        builder.Property(t => t.Institute).HasMaxLength(500).IsRequired();
    }
}