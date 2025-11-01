using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rooms.Persistence.EntityConfigurations.OperatorRoom;

public class OperatorRoomEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.OperatorRoom.OperatorRoom>
{
    public void Configure(EntityTypeBuilder<Domain.Models.OperatorRoom.OperatorRoom> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(x => x.Rooms).WithOne(x => x.OperatorRoom).HasForeignKey(x => x.OperatorRoomId);
        builder.Property(x => x.Operators).HasColumnType("jsonb");
        builder.Property(x => x.Contacts).HasMaxLength(50);
    }
}