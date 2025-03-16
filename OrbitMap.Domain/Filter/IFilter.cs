using System.Linq.Expressions;

namespace OrbitMap.Domain.Filter;

public interface IFilter<T>
{
    Expression<Func<T, bool>> ToExpression();
}