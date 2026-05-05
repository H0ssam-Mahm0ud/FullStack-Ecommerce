using Ecom.Domain.Filters;
using System.Linq.Expressions;

namespace Ecom.Application.Shared.Filters;

public class NumberConditionBuilder : ConditionBuilder
{
    public override FilterType SupportedType => FilterType.Number;

    public override Expression BuildCondition(Expression property, FilterRequestDto filter)
    {
        var op = filter.FilterOperator?.Trim();

        Expression condition = op switch
        {
            "GreaterThan" => BuildComparison(property, filter, ExpressionType.GreaterThan),
            "GreaterThanOrEqual" => BuildComparison(property, filter, ExpressionType.GreaterThanOrEqual),
            "LessThan" => BuildComparison(property, filter, ExpressionType.LessThan),
            "LessThanOrEqual" => BuildComparison(property, filter, ExpressionType.LessThanOrEqual),

            "Equals" => BuildComparison(property, filter, ExpressionType.Equal),

            "IsEmpty" => BuildIsEmpty(property),

            _ => BuildComparison(property, filter, ExpressionType.Equal)
        };

        return ApplyNotIfNeeded(condition, filter);
    }

    #region Core Builders

    private static Expression BuildComparison(Expression property, FilterRequestDto filter, ExpressionType comparisonType)
    {
        var constant = ParseNumber(property.Type, filter.FilterValue);

        return Expression.MakeBinary(comparisonType, property, constant);
    }

    private static Expression BuildIsEmpty(Expression property)
    {
        // Only valid for nullable types
        if (!IsNullable(property.Type))
            throw new InvalidOperationException("IsEmpty is only valid for nullable number types.");

        return Expression.Equal(property, Expression.Constant(null, property.Type));
    }

    #endregion

    #region Helpers

    private static Expression ParseNumber(Type type, string value)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        object parsedValue = underlyingType switch
        {
            Type t when t == typeof(int) => int.Parse(value),
            Type t when t == typeof(long) => long.Parse(value),
            Type t when t == typeof(float) => float.Parse(value),
            Type t when t == typeof(double) => double.Parse(value),
            Type t when t == typeof(decimal) => decimal.Parse(value),

            _ => throw new ArgumentException($"Unsupported numeric type: {type.Name}")
        };

        return Expression.Constant(parsedValue, type);
    }

    private static bool IsNullable(Type type)
        => Nullable.GetUnderlyingType(type) != null;

    private static Expression ApplyNotIfNeeded(Expression condition, FilterRequestDto filter)
        => filter.IsNot ? Expression.Not(condition) : condition;

    #endregion
}