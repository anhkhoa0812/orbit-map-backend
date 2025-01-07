using Contracts.Domains.Interface;

namespace Contracts.Domains;

public abstract class EntityAuditBase<T>: EntityBase<T>, IAuditable
{
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    
}