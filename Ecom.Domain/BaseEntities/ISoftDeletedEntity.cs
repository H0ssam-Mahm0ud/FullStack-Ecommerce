namespace Ecom.Domain.Entities;


/// <summary>
/// A contract for entities that support soft deletion.
/// </summary>
public interface ISoftDeletedEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been deleted.
    /// </summary>
    bool IsDeleted { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the entity was deleted.
    /// </summary>
    DateTime? DeletedAt { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the user who deleted the entity.
    /// </summary>
    Guid? DeletedBy { get; set; }
}
