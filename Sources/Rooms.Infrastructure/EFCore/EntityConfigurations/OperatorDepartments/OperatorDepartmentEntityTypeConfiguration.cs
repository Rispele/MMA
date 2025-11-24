using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.OperatorDepartments;

public class OperatorDepartmentEntityTypeConfiguration : IEntityTypeConfiguration<OperatorDepartment>
{
    public void Configure(EntityTypeBuilder<OperatorDepartment> builder)
    {
        builder.HasKey(department => department.Id);
        builder.Property(department => department.Id).HasField(OperatorDepartmentFieldNames.Id).ValueGeneratedOnAdd();

        builder.Property(department => department.Name).IsRequired().HasMaxLength(50);
        builder.Property(department => department.Contacts).HasMaxLength(50);

        builder.Ignore(department => department.Operators);
        builder.Property(OperatorDepartmentFieldNames.Operators).HasColumnType("jsonb");

        builder
            .HasMany(department => department.Rooms)
            .WithOne()
            .HasForeignKey(department => department.OperatorDepartmentId);
        builder
            .Navigation(department => department.Rooms)
            .HasField(OperatorDepartmentFieldNames.Rooms);
    }
}