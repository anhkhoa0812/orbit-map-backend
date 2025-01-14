using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class BusinessService : EntityBase<Guid>
{
    public EBusinessService BusinessServiceType { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Business>? Businesses { get; set; }
}