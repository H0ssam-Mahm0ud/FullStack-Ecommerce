namespace Ecom.Domain.Entities;


/// <summary>
/// Marker interface for entities.
/// </summary>
public interface IEntity
{
}


/// <summary>
/// Marker interface for single keyed entities.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public interface IEntity<TId> : IEntity
{
    public TId Id { get; set; }
}
