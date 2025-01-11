using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class News : EntityAuditBase<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> ImageUrls { get; set; }
    public string Author { get; set; }
    public string Source { get; set; }
    public int ReactionCount { get; set; }
}