using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Models.Room;
using Rooms.Infrastructure.EFCore.EntityConfigurations.Equipment;
using Rooms.Infrastructure.EFCore.EntityConfigurations.OperatorDepartments;
using Rooms.Infrastructure.EFCore.EntityConfigurations.Room;

namespace Rooms.Infrastructure.EFCore;

public class RoomsDbContext(DbContextOptions<RoomsDbContext> options) : DbContext(options)
{
    public DbSet<Room> Rooms { get; [UsedImplicitly] private set; }

    public DbSet<Equipment> Equipments { get; [UsedImplicitly] private set; }

    public DbSet<EquipmentSchema> EquipmentSchemas { get; [UsedImplicitly] private set; }

    public DbSet<EquipmentType> EquipmentTypes { get; [UsedImplicitly] private set; }

    public DbSet<OperatorDepartment> OperatorDepartments { get; [UsedImplicitly] private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EquipmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentSchemaEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoomEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperatorDepartmentEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}