using System.Linq.Expressions;

namespace Axiom.Application.Contracts.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default);
    Task<TResult?> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector,
        bool asNoTracking = false, CancellationToken ct = default);

    Task<IReadOnlyList<TEntity>> ListAllAsync(bool asNoTracking = false, CancellationToken ct = default);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>>? predicate,
        Expression<Func<TEntity, TResult>> selector, int skip = 0, int take = 50,
        bool asNoTracking = false, CancellationToken ct = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task DeleteAsync(TEntity entity, CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
}