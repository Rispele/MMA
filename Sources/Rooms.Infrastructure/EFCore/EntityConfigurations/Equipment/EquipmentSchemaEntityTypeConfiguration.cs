using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.Equipment;

internal class EquipmentSchemaEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentSchema>
{
    public void Configure(EntityTypeBuilder<EquipmentSchema> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasField(EquipmentSchemaFieldNames.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.ParameterValues).HasField(EquipmentSchemaFieldNames.ParameterValues).HasColumnType("jsonb");
    }
}