using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.EquipmentModels;
using Rooms.Domain.Models.RoomModels;
using Rooms.Domain.Queries.Abstractions;
using Rooms.Persistence.EntityConfigurations.Equipment;
using Rooms.Persistence.EntityConfigurations.Room;

namespace Rooms.Persistence;

public class RoomsDbContext(DbContextOptions<RoomsDbContext> options) : DbContext(options), IModelsSource
{
    public DbSet<Room> Rooms { get; [UsedImplicitly] private set; }

    public DbSet<Equipment> Equipments { get; [UsedImplicitly] private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EquipmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentSchemaEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoomEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}