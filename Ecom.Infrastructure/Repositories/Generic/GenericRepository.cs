using System.Linq.Expressions;
using Ecom.Domain.Contracts.Generic;
using Ecom.Domain.Entities;
using Ecom.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories.Generic;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : class , IEntity<TId>
{
    public GenericRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
        _dbSet = appDbContext.Set<TEntity>();
    }

    #region Fields
    protected readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;
    #endregion


    #region Properties
    public IQueryable<TEntity> Table => _dbSet.AsQueryable();
    public IQueryable<TEntity> TableNoTracking => _dbSet.AsNoTracking();
    #endregion


    #region Methods
    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();
        query = IncludeNavigationProperties(query);
        query = predicate == null ? query : query.Where(predicate);
        query = orderBy == null ? query : orderBy(query);
        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        var query = _dbSet.AsQueryable();
        query = IncludeNavigationProperties(query);
        return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(
        TId id,
        CancellationToken cancellationToken = default)
    {
        return await Table.AnyAsync(e => e.Id!.Equals(id), cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> SingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();
        query = IncludeNavigationProperties(query);
        return await query.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await _dbSet.AnyAsync(cancellationToken)
            : await _dbSet.AnyAsync(predicate, cancellationToken);
    }


    /// <inheritdoc/>
    public virtual async Task<bool> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.Update(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        catch
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
            throw;
        }
    }
    #endregion


    #region Protected Methods
    /// <inheritdoc/>
    protected virtual IQueryable<TEntity> IncludeNavigationProperties(IQueryable<TEntity> query)
    {
        return query;
    }
    #endregion
}
