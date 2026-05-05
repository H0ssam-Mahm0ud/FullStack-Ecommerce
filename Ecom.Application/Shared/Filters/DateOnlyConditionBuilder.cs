using Ecom.Domain.Filters;
using System.Linq.Expressions;

namespace Ecom.Application.Shared.Filters;

public class DateOnlyConditionBuilder : ConditionBuilder
{
    public override FilterType SupportedType => FilterType.DateOnly;

    public override Expression BuildCondition(Expression property, FilterRequestDto filter)
    {
        var propertyConverted = Expression.Convert(property, typeof(DateOnly));
        var value = DateOnly.FromDateTime(filter.Date);
        var constant = Expression.Constant(value, typeof(DateOnly));

        Expression condition = filter.FilterOperator switch
        {
            "Equals" => Expression.Equal(propertyConverted, constant),

            "GreaterThan" => Expression.GreaterThan(propertyConverted, constant),

            "GreaterThanOrEqual" => Expression.GreaterThanOrEqual(propertyConverted, constant),

            "LessThan" => Expression.LessThan(propertyConverted, constant),

            "LessThanOrEqual" => Expression.LessThanOrEqual(propertyConverted, constant),

            "Between" => BuildDateOnlyBetween(propertyConverted, filter),

            "IsEmpty" => BuildIsEmpty(property),

            _ => Expression.Equal(propertyConverted, constant)
        };

        return ApplyNotIfNeeded(condition, filter);
    }

    #region Helpers

    /// <summary>
    /// Builds an expression for a "Between" condition comparing a property with a range of DateOnly values.
    /// </summary>
    /// <param name="property">The property expression to be evaluated in the condition.</param>
    /// <param name="filter">The filter request that contains the start and end DateOnly values for comparison.</param>
    /// <returns>An expression representing the "Between" condition for DateOnly values.</returns>
    private static Expression BuildDateOnlyBetween(Expression property, FilterRequestDto filter)
    {
        Expression startDateNonNullableProperty = Expression.Convert(property, typeof(DateOnly));
        Expression endDateNonNullableProperty = Expression.Convert(property, typeof(DateOnly));

        ConstantExpression startDateValueExpression = Expression.Constant(DateOnly.FromDateTime(filter.StartDate), typeof(DateOnly));
        ConstantExpression endDateValueExpression = Expression.Constant(DateOnly.FromDateTime(filter.EndDate), typeof(DateOnly));
        var greaterThan = Expression.GreaterThanOrEqual(startDateNonNullableProperty, startDateValueExpression);
        var lessThan = Expression.LessThanOrEqual(endDateNonNullableProperty, endDateValueExpression);
        return Expression.AndAlso(greaterThan, lessThan);

    }

    private static Expression BuildIsEmpty(Expression property)
    {
        if (!IsNullable(property.Type))
            throw new InvalidOperationException("IsEmpty is only valid for nullable DateOnly.");

        return Expression.Equal(property, Expression.Constant(null, property.Type));
    }

    private static bool IsNullable(Type type)
        => Nullable.GetUnderlyingType(type) != null;

    private static Expression ApplyNotIfNeeded(Expression condition, FilterRequestDto filter)
        => filter.IsNot ? Expression.Not(condition) : condition;

    #endregion
}