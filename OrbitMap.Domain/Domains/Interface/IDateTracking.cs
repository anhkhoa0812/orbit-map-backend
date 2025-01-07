namespace Contracts.Domains.Interface;

public interface IDateTracking
{
    DateTime CreatedDate { get; set; }
    DateTime? LastModifiedDate { get; set; } 
}