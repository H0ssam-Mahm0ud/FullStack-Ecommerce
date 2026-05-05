
using Ecom.Application.Dtos;

namespace Ecom.Application.BaseDtos;


/// <summary>
/// Represents a data transfer object (DTO) that includes audit information.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public class AuditableDto<TId> : BaseDto<TId>
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the entity.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who last modified the entity.
    /// </summary>
    public Guid? ModifiedBy { get; set; }
}
