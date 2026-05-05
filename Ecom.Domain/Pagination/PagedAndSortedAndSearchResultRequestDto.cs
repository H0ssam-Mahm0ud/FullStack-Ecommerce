namespace Ecom.Domain.Pagination;


/// <summary>
/// Data transfer object used for paginated, sorted, and searchable queries.
/// Inherits pagination and sorting capabilities from <see cref="PagedAndSortedResultRequestDto"/>.
/// </summary>
public class PagedAndSortedAndSearchResultRequestDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Gets or sets the search term used to filter the results.
    /// </summary>
    public string SearchingTerm { get; set; }
}
