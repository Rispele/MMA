using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.EquipmentModels;

namespace Rooms.Persistence.EntityConfigurations.Equipment;

public class EquipmentSchemaEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentSchema>
{
    public void Configure(EntityTypeBuilder<EquipmentSchema> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Type).WithMany(x => x.Schemas).HasForeignKey(x => x.Id);
        builder.Property(x => x.ParameterValues).HasColumnType("jsonb");
    }
}