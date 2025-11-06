using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Models.Room;
using Rooms.Persistence.EntityConfigurations.Equipment;
using Rooms.Persistence.EntityConfigurations.OperatorDepartments;
using Rooms.Persistence.EntityConfigurations.Room;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence;

public class RoomsDbContext(DbContextOptions<RoomsDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Room> Rooms { get; [UsedImplicitly] private set; }

    public DbSet<Equipment> Equipments { get; [UsedImplicitly] private set; }

    public DbSet<EquipmentSchema> EquipmentSchemas { get; [UsedImplicitly] private set; }

    public DbSet<EquipmentType> EquipmentTypes { get; [UsedImplicitly] private set; }

    public DbSet<OperatorDepartment> OperatorDepartments { get; [UsedImplicitly] private set; }

    public IAsyncEnumerable<TEntity> ApplyQuery<TEntity>(IQuerySpecification<TEntity> querySpecification)
    {
        if (querySpecification is not IQueryImplementer<TEntity, RoomsDbContext> implementer)
        {
            throw new InvalidOperationException(
                $"QuerySpecification expected to be of type IQueryImplementer<TEntity, RoomsDbContext>, but was {querySpecification.GetType()}");
        }

        return implementer.Apply(this);
    }

    public Task<TEntity> ApplyQuery<TEntity>(
        ISingleQuerySpecification<TEntity> querySpecification,
        CancellationToken cancellationToken)
    {
        if (querySpecification is not ISingleQueryImplementer<TEntity, RoomsDbContext> implementer)
        {
            throw new InvalidOperationException(
                $"QuerySpecification expected to be of type ISingleQueryImplementer<TEntity, RoomsDbContext>, but was {querySpecification.GetType()}");
        }

        return implementer.Apply(this, cancellationToken);
    }

    public new void Add<TEntity>(TEntity entity)
        where TEntity : class
    {
        var set = base.Set<TEntity>();

        set.Add(entity);
    }

    public new void Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        var set = base.Set<TEntity>();

        set.Update(entity);
    }

    public Task Commit(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(cancellationToken);
    }

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