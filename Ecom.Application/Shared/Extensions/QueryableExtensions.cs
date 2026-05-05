using System.Linq.Expressions;
using System.Reflection;
using Ecom.Application.Shared.Filters;
using Ecom.Domain.Filters;
using Ecom.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Application.Shared.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IQueryable{T}"/>
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Applies pagination to the IQueryable.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the IQueryable.</typeparam>
    /// <param name="query">The IQueryable to apply pagination to.</param>
    /// <param name="pagination">The pagination parameters.</param>
    /// <returns>A paginated IQueryable.</returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedResultRequestDto pagination)
    {
        return query.Skip(pagination.SkipCount).Take(pagination.MaxResultCount);
    }


    /// <summary>
    /// Converts an IQueryable to a paginated result asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the IQueryable.</typeparam>
    /// <param name="query">The IQueryable to apply pagination to.</param>
    /// <param name="pagination">The pagination parameters.</param>
    /// <returns>
    /// A tuple containing the total count of items and the paginated list of items.
    /// </returns>
    public static async Task<(int totalCount, List<T> Items)> ToPagedResultAsync<T>(this IQueryable<T> query, PagedResultRequestDto pagination)
    {
        var totalCount = query.Count();
        query = query.Paginate(pagination);
        return (totalCount, await query.ToListAsync());
    }


    /// <summary>
    /// Orders the IQueryable based on the provided sorting expression.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the IQueryable.</typeparam>
    /// <param name="query">The IQueryable to apply sorting to.</param>
    /// <param name="sorting">The sorting expression in the format "PropertyName ASC|DESC".</param>
    /// <returns>
    /// An IQueryable ordered by the specified sorting expression.
    /// </returns>
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string? sorting)
    {
        if (string.IsNullOrWhiteSpace(sorting))
            return query;

        (var sortingExpression, string dir) = BuildExpression<T>.BuildSortingExpression(sorting);
        return dir == "DESC" ? query.OrderByDescending(sortingExpression) : query.OrderBy(sortingExpression);
    }

    /// <summary>
    /// Builds a search query that checks all string properties of the entity for the presence of the search term.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the IQueryable.</typeparam>
    /// <param name="query">The IQueryable to apply sorting to.</param>
    /// <param name="search">The search term.</param>
    /// <returns>
    /// An IQueryable filtered by the search term across all string properties.
    /// </returns>
    public static IQueryable<T> SearchBy<T>(this IQueryable<T> query, string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? predicate = null;

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead) continue;
            if (prop.PropertyType != typeof(string)) continue;

            var propertyAccess = Expression.Property(parameter, prop);

            // x.Prop != null
            var notNull = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));

            // x.Prop.ToLower().Contains(search.ToLower())
            var toLower = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
            var searchValue = Expression.Constant(search.ToLower());
            var contains = Expression.Call(toLower, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, searchValue);

            var condition = Expression.AndAlso(notNull, contains);

            predicate = predicate == null ? condition : Expression.OrElse(predicate, condition);
        }

        if (predicate == null)
            return query;

        var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
        return query.Where(lambda);
    }

    /// <summary>
    /// Applies a set of filters to the specified queryable data source.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the queryable data source.</typeparam>
    /// <param name="query">The queryable data source to which the filters will be applied.</param>
    /// <param name="filters">A list of filter criteria to apply. If <see langword="null"/>, the original query is returned unmodified.</param>
    /// <returns>
    /// A new <see cref="IQueryable{T}"/> that represents the filtered data source.
    /// </returns>
    public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, List<FilterRequestDto>? filters)
    {
        if (filters == null)
        {
            return query;
        }

        var filterExpression = BuildExpression<T>.BuildFilterExpression(filters);

        return query.Where(filterExpression);
    }
}
