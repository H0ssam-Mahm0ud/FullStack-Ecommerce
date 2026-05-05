using System.Linq.Expressions;
using Ecom.Domain.Filters;

namespace Ecom.Application.Shared.Filters;

public abstract class ConditionBuilder
{
    public abstract FilterType SupportedType { get; }
    /// <summary>
    /// Builds an expression based on the filter operator to compare a property with a date value.
    /// </summary>
    /// <param name="property">The property expression to be evaluated in the condition.</param>
    /// <param name="filter">The filter request that contains the filter operator and date values.</param>
    /// <returns>An expression representing the condition based on the filter operator.</returns>
    public abstract Expression BuildCondition(Expression property, FilterRequestDto filter);
}
