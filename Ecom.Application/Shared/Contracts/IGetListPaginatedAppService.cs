using Ecom.Domain.Filters;
using Ecom.Domain.Pagination;

namespace Ecom.Application.Shared.Contracts;

/// <summary>
/// Defines a service for retrieving a paginated list of items.
/// </summary>
/// <typeparam name="TInput">The type of the pagination request.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public interface IGetListPaginatedAppService<TInput, TOutput> where TInput : PagedResultRequestDto
{
    /// <summary>
    /// Gets a paginated list of items asynchronously.
    /// </summary>
    /// <param name="paginationRequest">The pagination request.</param>
    /// <param name="filters">A list of filters to apply.</param>
    /// <returns>
    /// A task that represents the asynchronous operation,
    /// the task result contains the pagination output.
    /// </returns>
    //Task<TOutput> GetAllPaginatedAsync(TInput paginationRequest, List<FilterRequestDto>? filters);
    Task<TOutput> GetAllPaginatedAsync(TInput paginationRequest);
}
