using Ecom.Domain.Filters;
using System.Linq.Expressions;
using System.Reflection;

namespace Ecom.Application.Shared.Filters;

public class StringConditionBuilder : ConditionBuilder
{
    public override FilterType SupportedType => FilterType.Text;

    public override Expression BuildCondition(Expression property, FilterRequestDto filter)
    {
        // IN operator
        if (filter.FilterOperator == "In" && filter.FilterValues?.Any() == true)
        {
            var inExpression = BuildInExpression(property, filter);
            return ApplyNotIfNeeded(inExpression, filter);
        }

        var propertyToLower = ToLower(property);
        var valueToLower = ToLower(Expression.Constant(filter.FilterValue ?? string.Empty));

        Expression condition = filter.FilterOperator switch
        {
            "Contains" => Call(propertyToLower, nameof(string.Contains), valueToLower),

            "Equals" => Expression.Equal(propertyToLower, valueToLower),

            "StartsWith" => Call(propertyToLower, nameof(string.StartsWith), valueToLower),

            "EndsWith" => Call(propertyToLower, nameof(string.EndsWith), valueToLower),

            "IsEmpty" => IsNullOrEmpty(property),

            _ => Expression.Equal(propertyToLower, valueToLower)
        };

        // Add null safety (only for non IsEmpty)
        if (filter.FilterOperator != "IsEmpty")
        {
            condition = AddNullSafety(property, condition);
        }

        return ApplyNotIfNeeded(condition, filter);
    }

    #region Helpers

    private static readonly MethodInfo ToLowerMethod =
        typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;

    private static readonly MethodInfo IsNullOrEmptyMethod =
        typeof(string).GetMethod(nameof(string.IsNullOrEmpty), new[] { typeof(string) })!;

    private static Expression ToLower(Expression exp)
        => Expression.Call(exp, ToLowerMethod);

    private static Expression Call(Expression instance, string method, Expression arg)
        => Expression.Call(instance, method, Type.EmptyTypes, arg);

    private static Expression IsNullOrEmpty(Expression property)
        => Expression.Call(IsNullOrEmptyMethod, property);

    private static Expression BuildInExpression(Expression property, FilterRequestDto filter)
    {
        var propertyToLower = ToLower(property);

        var values = filter.FilterValues
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v.ToLower())
            .ToList();

        var constant = Expression.Constant(values);
        var containsMethod = typeof(List<string>)
            .GetMethod(nameof(List<string>.Contains), new[] { typeof(string) })!;

        return Expression.Call(constant, containsMethod, propertyToLower);
    }

    private static Expression AddNullSafety(Expression property, Expression condition)
    {
        var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
        return Expression.AndAlso(notNull, condition);
    }

    private static Expression ApplyNotIfNeeded(Expression condition, FilterRequestDto filter)
        => filter.IsNot ? Expression.Not(condition) : condition;

    #endregion
}