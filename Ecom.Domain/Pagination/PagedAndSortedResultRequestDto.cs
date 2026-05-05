namespace Ecom.Domain.Pagination;


/// <summary>
/// Data transfer object that extends pagination with sorting capabilities.
/// Inherits paging functionality from <see cref="PagedResultRequestDto"/>.
/// </summary>
public class PagedAndSortedResultRequestDto : PagedResultRequestDto
{
    /// <summary>
    /// Gets or sets the sorting expression (e.g., "Name ASC", "CreatedDate DESC").
    /// </summary>
    public virtual string? Sorting { get; set; }
}
