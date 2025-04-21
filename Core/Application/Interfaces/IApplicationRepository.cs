using System.Security.Principal;
using OneIncUserAPI.Core.Domain.Common.Interfaces;
namespace OneIncUserAPI.Core.Application.Interfaces;

public interface IApplicationRepository<T> where T : class, IEntityBase
{
    IQueryable<T> Entities { get; }

    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(string id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
