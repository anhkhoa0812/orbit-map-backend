using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Role : EntityBase<Guid>
{
    public string Name { get; set; }
}