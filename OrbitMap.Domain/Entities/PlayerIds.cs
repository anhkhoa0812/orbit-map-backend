using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class PlayerIds : EntityBase<Guid>
{
    public string PlayerId { get; set; }
    public Member Member { get; set; }
    public string Username { get; set; }
}