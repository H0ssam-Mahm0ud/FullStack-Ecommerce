namespace Ecom.Application.Dtos;


/// <summary>
/// Represents a base data transfer object (DTO) with a unique identifier.
/// </summary>
/// <typeparam name="TId">The type of the identifier for the DTO.</typeparam>
public class BaseDto<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the DTO.
    /// </summary>
    public TId Id { get; set; }
}
