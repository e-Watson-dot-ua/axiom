namespace Axiom.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IReadOnlyDictionary<Type, object> Repositories { get; }
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}