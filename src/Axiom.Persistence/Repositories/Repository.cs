using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Axiom.Application.Contracts.Persistence;

namespace Axiom.Persistence.Repositories;

public class Repository<TEntity>(DbContext context)
    : IRepository<TEntity> where TEntity : class
{
	protected readonly DbContext _context = context;
	protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

	public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken ct = default)
	{
		var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
		return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
	}

	public virtual async Task<TResult?> GetByIdAsync<TResult>(Guid id,
		Expression<Func<TEntity, TResult>> selector, bool asNoTracking = false,
		CancellationToken ct = default)
	{
		var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
		return await query
			.Where(e => EF.Property<Guid>(e, "Id") == id)
			.Select(selector)
			.FirstOrDefaultAsync(ct);
	}

	public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync(bool asNoTracking = false,
        CancellationToken ct = default)
	{
		var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
		return await query.ToListAsync(ct);
	}

	public virtual async Task<IReadOnlyList<TResult>> ListAsync<TResult>(
		Expression<Func<TEntity, bool>>? predicate, Expression<Func<TEntity, TResult>> selector,
		int skip = 0, int take = 50, bool asNoTracking = false, CancellationToken ct = default)
	{
		IQueryable<TEntity> query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet;
		if (predicate != null)
			query = query.Where(predicate);

		return await query
			.Skip(skip)
			.Take(take)
			.Select(selector)
			.ToListAsync(ct);
	}

	public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
	{
		await _dbSet.AddAsync(entity, ct);
		return entity;
	}

	public virtual Task UpdateAsync(TEntity entity, CancellationToken ct = default)
	{
		_dbSet.Update(entity);
		return Task.CompletedTask;
	}

	public virtual Task DeleteAsync(TEntity entity, CancellationToken ct = default)
	{
		_dbSet.Remove(entity);
		return Task.CompletedTask;
	}

	public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
	{
		return await _dbSet.AnyAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
	}

	public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
	{
		return await _dbSet.AnyAsync(predicate, ct);
	}
}