using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Domain.Persistence;

public class EquipmentsDbContext(DbContextOptions<EquipmentsDbContext> options) : DbContext(options)
{
    public DbSet<Equipment> Equipments { get; [UsedImplicitly] private set; }
}