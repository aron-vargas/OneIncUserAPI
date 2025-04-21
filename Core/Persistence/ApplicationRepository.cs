using Microsoft.EntityFrameworkCore;
using OneIncUserAPI.Core.Application.Interfaces;
using OneIncUserAPI.Core.Domain.Common;

namespace OneIncUserAPI.Core.Persistence;

/// <summary>
/// A generic repository implementation for managing entities in the database.
/// </summary>
/// <typeparam name="T">The type of the entity, which must inherit from <see cref="EntityBase"/>.</typeparam>
public class ApplicationRepository<T> : IApplicationRepository<T> where T : EntityBase
{
    private readonly MemoryAppDB DBContext;

    /// <summary>
    /// Gets the queryable collection of entities of type <typeparamref name="T"/>.
    /// </summary>
    public IQueryable<T> Entities => DBContext.Set<T>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationRepository{T}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public ApplicationRepository(MemoryAppDB dbContext)
    {
        DBContext = dbContext;
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the entity fails validation.</exception>
    public async Task<T> AddAsync(T entity)
    {
        entity.ValidateInsert();
        await DBContext.Set<T>().AddAsync(entity);
        await DBContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the entity does not exist in the database.</exception>
    public async Task<T> UpdateAsync(T entity)
    {
        T? exist = DBContext.Set<T>().Find(entity.Id);
        if (exist == null)
        {
            throw new InvalidOperationException($"Entity with ID {entity.Id} not found.");
        }
        DBContext.Entry(exist).CurrentValues.SetValues(entity);
        entity.ValidateUpdate();
        await DBContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DeleteAsync(T entity)
    {
        DBContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/> from the database.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<List<T>> GetAllAsync()
    {
        return await DBContext.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Retrieves a queryable collection of all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing the entities.</returns>
    public IQueryable<T> GetAllQueriable()
    {
        return DBContext.Set<T>();
    }

    /// <summary>
    /// Retrieves an entity by its integer ID.
    /// </summary>
    /// <param name="id">The integer ID of the entity.</param>
    /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await DBContext.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Retrieves an entity by its string ID.
    /// </summary>
    /// <param name="id">The string ID of the entity.</param>
    /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
    public async Task<T?> GetByIdAsync(string id)
    {
        return await DBContext.Set<T>().FindAsync(id);
    }
}
