namespace Ecom.Domain.Entities;


/// <summary>
/// Marker interface for auditable entities.
/// </summary>
public interface IAuditableEntity : IEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the entity.
    /// </summary>
    Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who last modified the entity.
    /// </summary>
    Guid? ModifiedBy { get; set; }
}
