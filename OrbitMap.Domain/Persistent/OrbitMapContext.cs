using System.Reflection;
using Contracts.Domains.Interface;
using Microsoft.EntityFrameworkCore;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent;

public class OrbitMapContext : DbContext
{
    public OrbitMapContext()
    {
    }
    
    public OrbitMapContext(DbContextOptions<OrbitMapContext> options) : base(options)
    {
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Friendship> Friendship { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified ||
                        e.State == EntityState.Added ||
                        e.State == EntityState.Deleted);

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if(item.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.Now;
                        item.State = EntityState.Added;
                    }
                    break;
                case EntityState.Modified:
                    Entry(item.Entity).Property("Id").IsModified = false;
                    if(item.Entity is IDateTracking modifiedEntity)
                    {
                        modifiedEntity.LastModifiedDate = DateTime.Now;
                        item.State = EntityState.Modified;
                    }
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}