using System.Linq.Expressions;
using Ecom.Domain.Filters;

namespace Ecom.Application.Shared.Filters;

public static class BuildExpression<T>
{
    private static readonly FilterStrategy _filterStrategy;
    static BuildExpression()
    {
        var builders = new List<ConditionBuilder>
        {
            new NumberConditionBuilder(),
            new StringConditionBuilder(),
            new EnumConditionBuilder(),
            new DateConditionBuilder(),
            new DateOnlyConditionBuilder(),
            new TimeOnlyConditionBuilder()
        };

        _filterStrategy = new FilterStrategy(builders);
    }
    #region Public Methods
    public static Expression<Func<T, bool>> BuildFilterExpression(List<FilterRequestDto> filterDatas)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), "x");
        Expression combinedExpression = Expression.Constant(true);

        foreach (var filter in filterDatas)
        {
            if (string.IsNullOrEmpty(filter.FilterProperty) && string.IsNullOrEmpty(filter.FilterOperator))
                continue;

            Expression property = GetPropertyExpression(param, filter.FilterProperty);

            Expression condition = _filterStrategy.GetCondition(property, filter);
            combinedExpression = Expression.AndAlso(combinedExpression, condition);
        }

        return Expression.Lambda<Func<T, bool>>(combinedExpression, param);
    }

    public static (Expression<Func<T, object>> sortingExpression, string direction) BuildSortingExpression(string sorting)
    {
        if (string.IsNullOrWhiteSpace(sorting))
            return (x => x, "ASC");

        var parts = sorting.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var propertyPath = parts[0];
        var direction = parts.Length > 1 ? parts[1].ToUpperInvariant() : "ASC";

        ParameterExpression param = Expression.Parameter(typeof(T), "x");
        Expression property = GetPropertyExpression(param, propertyPath);

        UnaryExpression converted = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<T, object>>(converted, param);

        return (lambda, direction);
    }

    #endregion

    #region Private Methods

    private static Expression GetPropertyExpression(Expression param, string filtEcomroperty)
    {
        Expression property = param;

        foreach (var part in filtEcomroperty.Split('.'))
        {
            property = Expression.Property(property, part);
        }

        return property;
    }
    #endregion
}
