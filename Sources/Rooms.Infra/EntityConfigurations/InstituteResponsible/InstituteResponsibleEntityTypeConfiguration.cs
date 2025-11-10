using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rooms.Infra.EntityConfigurations.InstituteResponsible;

public class InstituteResponsibleEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.InstituteResponsible.InstituteResponsible>
{
    public void Configure(EntityTypeBuilder<Domain.Models.InstituteResponsible.InstituteResponsible> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Institute).HasMaxLength(500);
        builder.Property(t => t.Responsible).HasColumnType("jsonb");
    }
}