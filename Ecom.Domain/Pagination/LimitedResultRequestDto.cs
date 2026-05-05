using System.ComponentModel.DataAnnotations;

namespace Ecom.Domain.Pagination;

public class LimitedResultRequestDto
{
    // Default value: 10.
    public static int DefaultMaxResultCount { get; set; } = 10;

    // Default value: 1,000.
    public static int MaxMaxResultCount { get; set; } = 1000;

    // Maximum result count should be returned. This is generally used to limit result count on paging.
    [Range(1, int.MaxValue)]
    public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;
}
