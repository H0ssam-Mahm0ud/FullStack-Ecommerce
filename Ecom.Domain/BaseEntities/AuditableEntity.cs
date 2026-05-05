namespace Ecom.Domain.Entities;


/// <summary>
/// A base entity class that includes auditing and soft deletion properties.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity, ISoftDeletedEntity
{
    /// <inheritdoc/>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    /// <inheritdoc/>
    public DateTime? ModifiedAt { get; set; }
    /// <inheritdoc/>
    public Guid CreatedBy { get; set; }
    /// <inheritdoc/>
    public Guid? ModifiedBy { get; set; }

    /// <inheritdoc/>
    public bool IsDeleted { get; set; }
    /// <inheritdoc/>
    public DateTime? DeletedAt { get; set; }
    /// <inheritdoc/>
    public Guid? DeletedBy { get; set; }
}
