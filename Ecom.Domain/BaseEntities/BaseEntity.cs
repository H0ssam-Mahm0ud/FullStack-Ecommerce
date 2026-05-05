namespace Ecom.Domain.Entities;


/// <summary>
/// Base entity class that serves as a base for single keyed entities in the domain.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public abstract class BaseEntity<TId> : IEntity<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public TId Id { get; set; }
}
