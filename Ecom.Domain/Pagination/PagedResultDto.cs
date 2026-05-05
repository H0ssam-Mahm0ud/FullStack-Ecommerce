namespace Ecom.Domain.Pagination;

public class PagedResultDto<T> : ListResultDto<T>
{
    public int TotalCount { get; set; }

    /// <summary>
    /// Creates a new <see cref="PagedResultDto{T}"/> object.
    /// </summary>
    public PagedResultDto()
    {
    }

    /// <summary>
    /// Creates a new <see cref="PagedResultDto{T}"/> object.
    /// </summary>
    /// <param name="totalCount">Total count of Items</param>
    /// <param name="items">List of items in current page</param>
    public PagedResultDto(int totalCount, IReadOnlyList<T> items) : base(items)
    {
        TotalCount = totalCount;
    }
}
