using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Room;

namespace Rooms.Domain.Persistence;

public class DomainDbContext(DbContextOptions<DomainDbContext> options) : DbContext(options)
{
    public DbSet<Room> Rooms { get; [UsedImplicitly] private set; }
}