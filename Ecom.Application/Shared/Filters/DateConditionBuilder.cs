using Ecom.Domain.Filters;
using System.Linq.Expressions;

namespace Ecom.Application.Shared.Filters;

public class DateConditionBuilder : ConditionBuilder
{
    public override FilterType SupportedType => FilterType.Date;

    public override Expression BuildCondition(Expression property, FilterRequestDto filter)
    {
        var propertyConverted = Expression.Convert(property, typeof(DateTime));

        var date = filter.Date.Date;
        var startOfDay = Expression.Constant(date, typeof(DateTime));
        var endOfDay = Expression.Constant(date.AddDays(1), typeof(DateTime));

        Expression condition = filter.FilterOperator switch
        {
            "Equals" => BuildEquals(propertyConverted, startOfDay, endOfDay),

            "GreaterThan" => Expression.GreaterThan(propertyConverted, endOfDay),

            "GreaterThanOrEqual" => Expression.GreaterThanOrEqual(propertyConverted, startOfDay),

            "LessThan" => Expression.LessThan(propertyConverted, startOfDay),

            "LessThanOrEqual" => Expression.LessThanOrEqual(propertyConverted, endOfDay),

            "Between" => BuildDateBetween(propertyConverted, filter),

            "IsEmpty" => BuildIsEmpty(property),

            _ => BuildEquals(propertyConverted, startOfDay, endOfDay)
        };

        return ApplyNotIfNeeded(condition, filter);
    }

    #region Helpers

    private static Expression BuildEquals(Expression property, Expression start, Expression end)
    {
        return Expression.AndAlso(
            Expression.GreaterThanOrEqual(property, start),
            Expression.LessThan(property, end)
        );
    }

    /// <summary>
    /// Builds an expression for a "Between" condition comparing a property with a date range.
    /// </summary>
    /// <param name="property">The property expression to be evaluated in the condition.</param>
    /// <param name="filter">The filter request that contains the start and end date values for comparison.</param>
    /// <returns>An expression representing the "Between" condition.</returns>
    private static Expression BuildDateBetween(Expression property, FilterRequestDto filter)
    {
        Expression startDateNonNullableProperty = Expression.Convert(property, typeof(DateTime));
        Expression endDateNonNullableProperty = Expression.Convert(property, typeof(DateTime));

        ConstantExpression startDateValueExpression = Expression.Constant(filter.StartDate, typeof(DateTime));
        ConstantExpression endDateValueExpression = Expression.Constant(filter.EndDate.AddDays(1), typeof(DateTime));
        var greaterThan = Expression.GreaterThanOrEqual(startDateNonNullableProperty, startDateValueExpression);
        var lessThan = Expression.LessThanOrEqual(endDateNonNullableProperty, endDateValueExpression);
        return Expression.AndAlso(greaterThan, lessThan);

    }

    private static Expression BuildIsEmpty(Expression property)
    {
        if (!IsNullable(property.Type))
            throw new InvalidOperationException("IsEmpty is only valid for nullable DateTime.");

        return Expression.Equal(property, Expression.Constant(null, property.Type));
    }

    private static bool IsNullable(Type type)
        => Nullable.GetUnderlyingType(type) != null;

    private static Expression ApplyNotIfNeeded(Expression condition, FilterRequestDto filter)
        => filter.IsNot ? Expression.Not(condition) : condition;

    #endregion
}