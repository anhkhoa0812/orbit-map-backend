using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Filter.FilterModel;

public class TravelPlanFilter : IFilter<TravelPlan>
{
    public string? LocationName { get; set; }
    public ETravelPlanType? Type { get; set; }

    public Expression<Func<TravelPlan, bool>> ToExpression()
    {
        return x => (string.IsNullOrEmpty(LocationName) ||
                     x.Location.Name.Trim().ToLower().Contains(LocationName.Trim().ToLower())) &&
                    (!Type.HasValue || x.Type == Type);
    }
}