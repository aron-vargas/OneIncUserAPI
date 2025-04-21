using Microsoft.EntityFrameworkCore;

namespace OneIncUserAPI.Core;

public class ApplicationRepository<T> where T : EntityBase
{
    private readonly MemoryAppDB DBContext;
    public IQueryable<T> Entities => DBContext.Set<T>();

    public ApplicationRepository(MemoryAppDB dbContext)
    {
        DBContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.ValidateInsert(new AppUser());
        await DBContext.Set<T>().AddAsync(entity);
        await DBContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        T? exist = DBContext.Set<T>().Find(entity.Id);
        if (exist == null)
        {
            // Handle null case explicitly
            throw new InvalidOperationException($"Entity with ID {entity.Id} not found.");
        }
        DBContext.Entry(exist).CurrentValues.SetValues(entity);
        entity.ValidateUpdate(new AppUser());
        await DBContext.SaveChangesAsync();
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        DBContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DBContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DBContext.Set<T>().FindAsync(id);
    }
    public async Task<T?> GetByIdAsync(string id)
    {
        return await DBContext.Set<T>().FindAsync(id);
    }
}