using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Persistence.EntityConfigurations.OperatorDepartments;

public class OperatorDepartmentEntityTypeConfiguration : IEntityTypeConfiguration<OperatorDepartment>
{
    public void Configure(EntityTypeBuilder<OperatorDepartment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(x => x.Rooms).WithOne().HasForeignKey(x => x.OperatorDepartmentId);
        builder.Property(x => x.Operators).HasColumnType("jsonb");
        builder.Property(x => x.Contacts).HasMaxLength(50);
    }
}