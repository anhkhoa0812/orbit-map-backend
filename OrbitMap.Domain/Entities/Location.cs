using System.ComponentModel.DataAnnotations;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Location : EntityBase<string>
{
    [Required] public string Name { get; set; }
    [Required] public string Image { get; set; }

    public virtual ICollection<MemberLocation>? MemberLocations { get; set; }

    public virtual ICollection<Business>? Businesses { get; set; }
}