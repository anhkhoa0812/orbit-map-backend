using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class NewsReactionConfiguration : IEntityTypeConfiguration<NewsReaction>
{
    public void Configure(EntityTypeBuilder<NewsReaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(nr => nr.News)
            .WithMany(n => n.NewsReactions)
            .HasForeignKey(nr => nr.NewsId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(nr => nr.Member)
            .WithMany(u => u.NewsReactions)
            .HasForeignKey(nr => nr.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(nr => nr.ReactionType)
            .HasConversion(
                v => v.ToString(),
                v => (EReactionType)Enum.Parse(typeof(EReactionType), v));
    }
}