using System.Linq.Expressions;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Filter.FilterModel;

public class NewsFilter : IFilter<News>
{
    public string? BusinessName { get; set; }
    public ENewsType? Type { get; set; }

    public Expression<Func<News, bool>> ToExpression()
    {
        return news =>
            (string.IsNullOrEmpty(BusinessName) || news.BusinessName.Contains(BusinessName)) &&
            (!Type.HasValue || news.Type == Type);
    }
}