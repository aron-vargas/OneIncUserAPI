using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OneIncUserAPI.Core.Domain.Models;

namespace OneIncUserAPI.Core.Persistence;

/// <summary>
/// Represents an in-memory database context for the application.
/// </summary>
public class MemoryAppDB : DbContext
{
    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for managing <see cref="AppUser"/> entities.
    /// </summary>
    public DbSet<AppUser> Users => Set<AppUser>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for managing <see cref="AppRole"/> entities.
    /// </summary>
    public DbSet<AppRole> Roles => Set<AppRole>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for managing <see cref="UserRole"/> entities.
    /// </summary>
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryAppDB"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public MemoryAppDB(DbContextOptions<MemoryAppDB> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the database context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
