using System.Linq.Expressions;
using Ecom.Domain.Entities;

namespace Ecom.Domain.Contracts.Generic;

/// <summary>
/// Generic repository interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the database entity.</typeparam>
public interface IGenericRepository<TEntity, TId> where TEntity : IEntity<TId>
{
    /// <summary>
    /// Gets the queryable collection of entities.
    /// </summary>
    IQueryable<TEntity> Table { get; }

    /// <summary>
    /// Gets the queryable collection of entities without tracking changes.
    /// </summary>
    IQueryable<TEntity> TableNoTracking { get; }

    /// <summary>
    /// Retrieves all entities matching the specified predicate and order.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities upon.</param>
    /// <param name="orderBy">The ordering function to order entities upon</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of entities that match the specified criteria.
    /// </returns>
    Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.
    /// </returns>
    Task<TEntity?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the entity was found; otherwise, false.
    /// </returns>
    Task<bool> ExistsAsync(
        TId id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first entity that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities upon.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the first entity that matches the predicate, or null if no entity matches.
    /// </returns>
    Task<TEntity?> SingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the number of entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities upon.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the count of entities that match the predicate.
    /// </returns>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities upon.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if any entity matches the predicate; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts a new entity into the repository.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the insert operation succeded; otherwise, false.
    /// </returns>
    Task<bool> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the update operation succeded; otherwise, false.
    /// </returns>
    Task<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the delete operation succeded; otherwise, false.
    /// </returns>
    Task<bool> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
}
