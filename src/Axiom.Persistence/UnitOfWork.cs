using System.Collections.Concurrent;
using Axiom.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Axiom.Persistence.Repositories;

namespace Axiom.Persistence;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
	private readonly DbContext _context = context;
	private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public IReadOnlyDictionary<Type, object> Repositories => _repositories;

	public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
	{
		var type = typeof(TEntity);
		if (_repositories.TryGetValue(type, out var repo))
			return (IRepository<TEntity>)repo;

		var repository = new Repository<TEntity>(_context);
		_repositories[type] = repository;
		return repository;
	}

	public async Task<int> SaveChangesAsync(CancellationToken ct = default)
		=> await _context.SaveChangesAsync(ct);

	public void Dispose()
	{
		_context.Dispose();
		GC.SuppressFinalize(this);
	}

	public async ValueTask DisposeAsync()
	{
		await _context.DisposeAsync();
		GC.SuppressFinalize(this);
	}
}