using Ecom.Domain.Filters;
using System.Linq.Expressions;

namespace Ecom.Application.Shared.Filters;

public class EnumConditionBuilder : ConditionBuilder
{
    public override FilterType SupportedType => FilterType.Enum;
    public override Expression BuildCondition(Expression property, FilterRequestDto filter)
    {
        Type propertyType = property.Type;
        ConstantExpression valueNumber = Expression.Constant(Convert.ChangeType(Enum.Parse(propertyType, filter.FilterValue.ToString()), propertyType));
        return Expression.Equal(property, valueNumber);
    }
}
