using Ecom.Domain.Filters;
using System.Linq.Expressions;

namespace Ecom.Application.Shared.Filters;

internal class FilterStrategy
{
    private readonly Dictionary<FilterType, ConditionBuilder> _builders;

    public FilterStrategy(IEnumerable<ConditionBuilder> builders)
    {
        _builders = builders.ToDictionary(b => b.SupportedType);
    }

    public Expression GetCondition(Expression property, FilterRequestDto filter)
    {
        if (_builders.TryGetValue(filter.TypeFilter, out var builder))
        {
            return builder.BuildCondition(property, filter);
        }
        throw new Exception($"No condition builder found for filter type: {filter.TypeFilter}.");
    }
}
