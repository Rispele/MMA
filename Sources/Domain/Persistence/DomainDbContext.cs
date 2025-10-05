using Domain.Models;
using Domain.Models.Room;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence;

public class DomainDbContext(DbContextOptions<DomainDbContext> options) : DbContext(options)
{
    public DbSet<Room> Rooms { get; [UsedImplicitly] private set; }

    public DbSet<FileDescriptor> Files { get; [UsedImplicitly] private set; }
}